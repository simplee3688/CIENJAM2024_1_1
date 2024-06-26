using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected bool isRegenable;
    [SerializeField] protected float regenCoolTime;

    public virtual void GetItemEvent()
    {
        active(false);
        if (isRegenable) StartCoroutine(waitRegenTime());
    }

    
    IEnumerator waitRegenTime()
    {
        float nowTime = regenCoolTime;
        yield return new WaitForSeconds(nowTime);
        active(true);
    }

    private void active(bool isActive)
    {
        GetComponent<SpriteRenderer>().enabled = isActive;
        GetComponent<BoxCollider2D>().enabled = isActive;
    }
}
