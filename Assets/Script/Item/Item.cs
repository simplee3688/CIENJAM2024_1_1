using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected bool isRegenable;
    [SerializeField] protected float regenCoolTime;

    public virtual void GetItemEvent()
    {

        if (isRegenable) StartCoroutine(waitRegenTime());
    }

    
    IEnumerator waitRegenTime()
    {
        float nowTime = regenCoolTime;
        yield return new WaitForSeconds(nowTime);
    }
}
