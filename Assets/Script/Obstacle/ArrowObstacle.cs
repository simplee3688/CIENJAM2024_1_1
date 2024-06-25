using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObstacle : Obstacle
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float liveTime;

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if(liveTime <= 0)
        {
            Destroy(this.gameObject);
        }
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public void Init(Vector2 direction, float liveTime, float speed)
    {
        this.direction = direction;
        this.liveTime = liveTime;
        this.speed = speed;
    }
}
