using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float gracePeriod;
    
    [SerializeField] List<Buf> bufs = new List<Buf>();
    

    public DamageInfo GetDamageInfo()
    {
        List<Buf> tmp = new List<Buf>();
        foreach(var b in bufs)
        {
            tmp.Add((Buf)b.Clone());
        }

        DamageInfo damageInfos = new DamageInfo(damage, gracePeriod, tmp);
        
        return damageInfos;
    }
}
