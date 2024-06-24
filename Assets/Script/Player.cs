using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float gravityScale;
    [SerializeField] float dashPower;

    [SerializeField] float p_x;
    [SerializeField] float p_y;
    [SerializeField] float dashForce;

    [SerializeField] bool isFloor;
    [SerializeField] int dir = 1;
    [SerializeField] int maxJumpCount = 1;
    [SerializeField] int maxDashCount = 1;

    int jumpCount = 0;
    int dashCount = 0;

    Vector3 moveVec;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
        if(this.rigid == null)
        {
            this.rigid = this.AddComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position - Vector3.up * 0.45f, new Vector2(0.8f, 0.15f), 0, Vector2.zero, 0, 1 << 3);
        if(hit.collider != null)
        {
            if (!isFloor)
            {
                jumpCount = maxJumpCount;
                p_y = 0;
                isFloor = true;
            }
        }
        else
        {
            isFloor = false;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            dir = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1; 
            p_x = Input.GetAxisRaw("Horizontal") * speed;
        }

        hit = Physics2D.BoxCast(this.transform.position + dir * Vector3.right * 0.45f, new Vector2(0.15f, 0.8f), 0, Vector2.zero, 0, 1 << 3);
        if (hit.collider != null)
        {
            p_x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isFloor && jumpCount > 0)
        {
            jumpCount--;
            p_y = jumpPower;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0)
        {
            dashCount--;
            dashForce = dashPower;
        }
        else if(isFloor)
        {
            dashCount = maxDashCount;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position - Vector3.up * 0.45f, new Vector2(0.8f, 0.12f));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position + dir * Vector3.right * 0.45f, new Vector2(0.15f, 0.8f));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position + Vector3.right * dir * 0.5f, this.transform.position + Vector3.right * dir * 0.5f + moveVec * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        moveVec = new Vector3(p_x + dashForce * dir, p_y, 0);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + Vector3.right * dir * 0.5f, moveVec.normalized, Vector2.Distance(Vector2.zero, moveVec * Time.deltaTime), 1 << 3);
        if(hit)
        {
            rigid.MovePosition(hit.point - dir * Vector2.right * 0.5f);
        }
        else
        {
            rigid.MovePosition(this.transform.position + moveVec * Time.deltaTime);
        }

        p_x *= 0.85f;
        dashForce *= 0.85f;
        if (!isFloor)
        {
            p_y -= gravityScale;
        }
    }
}
