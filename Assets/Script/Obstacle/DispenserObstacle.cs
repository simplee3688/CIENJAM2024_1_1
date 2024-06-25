using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserObstacle : Obstacle
{
    [SerializeField] ArrowObstacle arrow;

    [SerializeField] float coolTime;
    [SerializeField] float currentTime;

    [SerializeField] Vector3 arrowDir;
    [SerializeField] float arrowLiveTime;
    [SerializeField] float arrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(arrow == null)
        {
            Destroy(this.gameObject);
        }

        currentTime = coolTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + arrowDir.normalized * arrowLiveTime * arrowSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime < 0) {
            ArrowObstacle obstacle = Instantiate(arrow, this.transform.position, Quaternion.identity);
            obstacle.Init(arrowDir.normalized, arrowLiveTime, arrowSpeed);
            obstacle.gameObject.SetActive(true);

            currentTime = coolTime;
        }
    }
}
