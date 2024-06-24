using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float gracePeriod;
    
    [SerializeField] List<Buf> bufs = new List<Buf>();
    

    public DamageInfo[] GetDamageInfo()
    {
        DamageInfo[] damageInfos = new DamageInfo[bufs.Count];
        for (int i = 0; i < bufs.Count; i++)
        {
            
            damageInfos[i] = new DamageInfo(damage, gracePeriod, (Buf)bufs[i].Clone());
        }
        
        return damageInfos;
    }
}
