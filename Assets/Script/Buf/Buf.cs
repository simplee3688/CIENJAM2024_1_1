using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buf : ICloneable
{
    [SerializeField] BufEnum bufEnum;
    [SerializeField] bool isPassive;
    [SerializeField] float stateDuration;
    [SerializeField] float strength;

    public Buf(BufEnum bufEnum, float stateDuration, float strength, bool isPassive = false)
    {
        this.bufEnum = bufEnum;
        this.stateDuration = stateDuration;
        this.strength = strength;
        this.isPassive = isPassive;
    }

    public object Clone()
    {
        return new Buf(bufEnum, stateDuration, strength);
    }
    public float UpdateBuf(float time, out bool isContinue, out BufEnum bufEnum) 
    {
        if(!isPassive) stateDuration -= time;
        if (stateDuration < 0) isContinue = false;
        else isContinue = true;
        bufEnum = this.bufEnum;
        return strength;
    }
    override public string ToString()
    {
        return bufEnum.ToString() + " " + stateDuration + " " + strength;
    }
}
