using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected bool isRegenable;
    [SerializeField] protected float regenCoolTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetItemEvent()
    {

    }

    IEnumerator waitRegenTime()
    {
        float nowTime = regenCoolTime;
        yield return null;
    }
}
