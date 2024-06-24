using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTextTime : MonoBehaviour
{
    float timer;
    public void setTimer(float timer)
    {
        this.timer = timer;
        StartCoroutine(Timer(timer));
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
