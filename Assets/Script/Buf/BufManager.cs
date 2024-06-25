using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BufManager
{
    [SerializeField]
    public List<Buf> bufList = new List<Buf>();
    Dictionary<BufEnum, float> bufPercent = new Dictionary<BufEnum, float>();
    public Dictionary<BufEnum, float> BufPercent => bufPercent;

    public BufManager()
    {
        for(int i = 1; i < Enum.GetValues(typeof(BufEnum)).Length; i++)
        {
            bufPercent.Add((BufEnum)i, 100f);
        }
    }


    public void UpdateBufList(float time)
    {
        foreach (var key in bufPercent.Keys.ToList()) bufPercent[key] = 100f;

        for(int i = 0; i < bufList.Count; i++)
        {
            bool isBufContinue; BufEnum bufEnum;
            float bufStrength = bufList[i].UpdateBuf(time, out isBufContinue, out bufEnum);
            if (!isBufContinue)
            {
                bufList.Remove(bufList[i]);
                continue;
            }
            bufPercent[bufEnum] += bufStrength;
        }
    }

    public void Add(Buf buf)
    {
        bufList.Add(buf);
        Testcode();
    }

    public void Add(List<Buf> bufList)
    {
        for(int i = 0; i < bufList.Count; i++)
        {
            Add(bufList[i]);
        }
    }

    public IEnumerator BufManagerCoroutine(float time)
    {
        while (true)
        {
            UpdateBufList(time);
            yield return new WaitForSeconds(time);
            foreach (var key in bufPercent.Keys.ToList())
            {
                Debug.Log(key + " : " + bufPercent[key]);
            }
            
        }
    }

    private void Testcode()
    {
        for(int i = 0;i < bufList.Count;i++)
        {
            Debug.Log(bufList[i].ToString());
        }
    }
}
