using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    float damage { get; }
    float gracePeriod { get; }
    public List<Buf> bufList { get; private set; }

    public DamageInfo(float damage, float gracePeriod, List<Buf> bufList)
    {
        this.damage = damage;
        this.gracePeriod = gracePeriod;
        this.bufList = bufList;
    }

    public DamageInfo(float damage, float gracePeriod, Buf buf)
    {
        this.damage = damage;
        this.gracePeriod = gracePeriod;
        this.bufList = new List<Buf> { buf };
    }


}
