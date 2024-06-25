using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float gravityScale;
    [SerializeField] float dashPower;

    [SerializeField] bool isFloor;
    [SerializeField] int dir = 1;
    [SerializeField] int maxJumpCount = 1;
    [SerializeField] int maxDashCount = 1;

    [SerializeField] float input_x;
    [SerializeField] float p_x;
    [SerializeField] float p_y;
    [SerializeField] float dashForce;

    [SerializeField] int jumpCount = 0;
    [SerializeField] int dashCount = 0;

    [SerializeField] float groundAngle;
    [SerializeField] float wallAngle;

    [SerializeField] Vector3 moveVec;
    List<ContactPoint2D> contactList;

    [SerializeField] Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] BufManager bufManager;
    private float bufUpdateTime;

    [SerializeField] float graceTime;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
        if(this.rigid == null)
        {
            this.rigid = this.AddComponent<Rigidbody2D>();
        }
        this.animator = this.GetComponent<Animator>();
        contactList = new List<ContactPoint2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        dir = 0;
        bufUpdateTime = GameManager.Instance.bufUpdateTime;

        StartCoroutine(bufManager.BufManagerCoroutine(bufUpdateTime)); //버프매니저 코루틴 실행
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            dir = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;
            input_x = Input.GetAxisRaw("Horizontal") * speed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0 && Mathf.Abs(dashForce) < 1)
        {
            dashCount--;
            dashForce = dashPower * dir;
        }
        else if (isFloor)
        {
            dashCount = maxDashCount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            p_y = jumpPower * Mathf.Clamp(bufManager.BufPercent[BufEnum.jumpPowerPercent], 20, 200) / 100;
        }

        graceTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float unGravityForce = 0;
        rigid.GetContacts(contactList);
        //Debug.Log("Count : " + contactList.Count);
        if (contactList.Count > 0)
        {
            float groundCos = Mathf.Cos(groundAngle / 180 * Mathf.PI);
            float wallCos = Mathf.Cos(wallAngle / 180 * Mathf.PI);
            bool tmpIsFloor = false;

            foreach (ContactPoint2D contactPoint in contactList)
            {
                float groundCheck = Vector2.Dot(Vector2.up, contactPoint.normal.normalized);
                //Debug.Log("groundChek : " + groundCheck + ", groundCos : " + groundCos);
                float wallCheck = Vector2.Dot(Vector2.right, contactPoint.normal.normalized);
                //Debug.Log("wallChek : " + wallCheck + ", wallCos : " + wallCos);
                
                if (groundCheck > groundCos)
                {
                    tmpIsFloor = true;

                    if (!isFloor)
                    {
                        jumpCount = maxJumpCount;
                        p_y = 0;
                    }
                }
                else if (Mathf.Abs(wallCheck) > wallCos)
                {
                    dashForce = 0;
                    if (wallCheck < 0)
                    {
                        input_x = Mathf.Min(0, input_x);
                        p_x = Mathf.Min(0, p_x);
                    }
                    else
                    {
                        input_x = Mathf.Max(0, input_x);
                        p_x = Mathf.Max(0, p_x);
                    }
                }
                unGravityForce = Mathf.Max(contactPoint.normal.normalized.y, unGravityForce);
            }
            isFloor = tmpIsFloor;
        }
        else
        {
            isFloor = false;
            p_x *= 0.85f;
        }

        //Debug.Log("p_y : " + p_y + ", isFloor : " + isFloor + ", minSlide : " + minSlide);
        moveVec = new Vector3((input_x + p_x + dashForce) * Mathf.Clamp(bufManager.BufPercent[BufEnum.speedPercent], 20, 200) / 100, p_y, 0);
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position, moveVec.normalized, moveVec.magnitude * Time.deltaTime, 1 << 3);
        if(ray)
        {
            rigid.MovePosition(ray.point - 0.35f * (ray.point - rigid.position));
        }
        else
        {
            rigid.MovePosition(this.transform.position + moveVec * Time.deltaTime);
        }

        animator.SetFloat("movement", moveVec.magnitude); 
        animator.SetInteger("Direction", dir);


        input_x *= 0.85f;
        dashForce *= 0.85f;
        if (!isFloor)
        {
            //Debug.Log(minSlide);
            p_y -= gravityScale * (1 - unGravityForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + moveVec.normalized * 2);

        if (rigid != null && contactList != null)
        {
            Gizmos.color = Color.black;
            foreach (ContactPoint2D contactPoint in contactList)
            {
                Gizmos.DrawLine(contactPoint.point, contactPoint.point + contactPoint.normal * 2);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("isTrigger");
        if(collision.tag == "Obstacle")
        {
            Debug.Log("It's Obsacle!");
            if (graceTime <= 0)
            {
                var damageInfo = collision.GetComponent<Obstacle>().GetDamageInfo();
                graceTime = damageInfo.gracePeriod;
                foreach(Buf buf in damageInfo.bufList)
                {
                    bufManager.Add(buf);
                }
                StartCoroutine("damageEffect");
            }
        }
    }

    IEnumerator damageEffect()
    {
        float alpha = 1;
        int direction = -1;
        Color newColor = spriteRenderer.color;
        while (graceTime > 0)
        {
            newColor = spriteRenderer.color;
            alpha += direction * 0.35f;
            if(alpha > 1 || alpha < 0)
            {
                direction *= -1;
                alpha = Mathf.Clamp(alpha, 0, 1);
            }
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
        newColor.a = 1;
        spriteRenderer.color = newColor;
    }
}
