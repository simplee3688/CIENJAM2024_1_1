using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponeArea : MonoBehaviour
{
    [SerializeField] Transform responePoint;
    [SerializeField] float cost;

    public Vector3 getRespawnPosition()
    {
        Debug.Log(responePoint.position);
        return responePoint.position;
    }

    public float getCost()
    {
        return cost;
    }
}
