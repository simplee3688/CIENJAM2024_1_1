using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
        if(this.rigid == null)
        {
            this.rigid = this.AddComponent<Rigidbody2D>();
        }
        contactList = new List<ContactPoint2D>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            dir = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;
            input_x = Input.GetAxisRaw("Horizontal") * speed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0)
        {
            dashCount--;
            dashForce = dashPower;
        }
        else if (isFloor)
        {
            dashCount = maxDashCount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            p_y = jumpPower;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float minSlide = 1;
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
                    if (wallCheck < 0)
                    {
                        input_x = Mathf.Min(0, input_x);
                        p_x = Mathf.Min(0, p_x);
                        dashForce = Mathf.Min(0, dashForce);
                    }
                    else
                    {
                        input_x = Mathf.Max(0, input_x);
                        p_x = Mathf.Max(0, p_x);
                        dashForce = Mathf.Max(0, dashForce);
                    }
                }
                else if(contactPoint.normal.normalized.y > 0)
                {
                    minSlide = Mathf.Min(contactPoint.normal.normalized.y, minSlide);
                    Debug.Log("Vector : " + contactPoint.normal +  ", normal Vector : " + contactPoint.normal.normalized + ", minSlide Change : " + minSlide);
                }
            }
            isFloor = tmpIsFloor;
        }
        else
        {
            isFloor = false;
            p_x *= 0.85f;
        }

       Debug.Log("p_y : " + p_y + ", isFloor : " + isFloor + ", minSlide : " + minSlide);
        moveVec = new Vector3(input_x + p_x + dashForce * dir, p_y, 0);
        rigid.MovePosition(this.transform.position + moveVec * Time.deltaTime);

        input_x *= 0.85f;
        dashForce *= 0.85f;
        if (!isFloor)
        {
            //Debug.Log(minSlide);
            p_y -= gravityScale * minSlide;
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

}
