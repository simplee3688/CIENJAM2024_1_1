using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour, TimeGetAdapter
{
    [SerializeField] private float remainTime;

    public float getRemainTime()
    {
        return remainTime;
    }

    public void reduceTime(float time)
    {
        remainTime -= time;
    }

    private void CheckTimeOut()
    {
        if (remainTime <= 0)
        {
            GameManager.Instance.Gameover();
        }
    }

    public void setting(float time)
    {
        remainTime = time;
    }
}
