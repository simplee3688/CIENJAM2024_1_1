using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : Obstacle
{
    [SerializeField] float startHeight;
    [SerializeField] float endHeight;

    [SerializeField] float respawnTime;
    [SerializeField] float p_y;
    [SerializeField] float gravityScale;

    bool isActive;

    Rigidbody2D rigid;
    Renderer render;
    

    private void Start()
    {
        render = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.MovePosition(new Vector2(this.rigid.position.x, startHeight));
        isActive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x - 0.5f, startHeight + 0.5f), new Vector3(transform.position.x + 0.5f, startHeight + 0.5f));
        Gizmos.DrawLine(new Vector3(transform.position.x - 0.5f, endHeight - 0.5f), new Vector3(transform.position.x + 0.5f, endHeight - 0.5f));
    }

    private void FixedUpdate()
    {
        if(isActive)
        {
            if (this.rigid.position.y < endHeight)
            {
                p_y = 0;
                rigid.MovePosition(new Vector2(this.rigid.position.x, startHeight));
                render.enabled = false;
                isActive = false;
                StartCoroutine("Respawn");
            }
            else
            {
                Vector2 moveVec = new Vector2(0, p_y);
                rigid.MovePosition(this.rigid.position + moveVec * Time.deltaTime);
            }

            p_y -= gravityScale;
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        isActive = true;
        render.enabled = true;
    }
}
