using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class WanderingObstacle : Obstacle
{
    [SerializeField] Vector2 pos1;
    [SerializeField] Vector2 pos2;

    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    [SerializeField] bool isGoPos1;

    float p_x;
    float p_y;

    Rigidbody2D rigid;
    Vector2 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        isGoPos1 = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos1, 0.1f);
        Gizmos.DrawWireSphere(pos2, 0.1f);
    }

    private void FixedUpdate()
    {
        Vector2 goal = isGoPos1 ? pos1 : pos2;
        p_x = ((goal.x - rigid.position.x) > 0 ? 1 : -1) * speed;
        p_y = 0;

        moveVec = new Vector2(p_x, p_y);
        rigid.MovePosition(this.rigid.position + moveVec * Time.deltaTime);

        if(Vector2.Distance(goal, rigid.position) < 0.5)
        {
            isGoPos1 = !isGoPos1;
        }
    }
}
