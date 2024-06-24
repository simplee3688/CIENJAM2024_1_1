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
    [SerializeField] int dir;
    [SerializeField] int maxJumpCount = 1;
    [SerializeField] int maxDashCount = 1;

    int jumpCount = 0;
    int dashCount = 0;

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
            int dir = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;
            hit = Physics2D.BoxCast(this.transform.position + dir * Vector3.right * 0.45f, new Vector2(0.12f, 0.8f), 0, Vector2.zero, 0, 1 << 3);
            if (hit.collider != null)
            {
                p_x = 0;
            }
            else
            {
                p_x = Input.GetAxisRaw("Horizontal") * speed;
            }
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
    }

    private void FixedUpdate()
    {
        if(p_x != 0)
        {
            dir = p_x > 0 ? 1 : -1;
        }
        Vector3 moveVec = new Vector3(p_x + dashForce * dir, p_y, 0);
        rigid.MovePosition(this.transform.position + moveVec * Time.deltaTime);

        p_x *= 0.85f;
        dashForce *= 0.85f;
        if (!isFloor)
        {
            p_y -= gravityScale;
        }
    }
}
