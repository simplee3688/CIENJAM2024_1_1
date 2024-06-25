using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABSelectUI : MonoBehaviour
{
    [SerializeField]
    Buf[] selectABuf;
    [SerializeField]
    Buf[] selectBBuf;
    public void SelectEventStart()
    {
        gameObject.SetActive(true);
    }

    public void SelectEventEnd()
    {
        gameObject.SetActive(false);
    }

    public void SelectButton(int index)
    {
        Buf[] bufList;
        if (index == 0)
        {
            bufList = new Buf[selectABuf.Length];
            for (int i = 0; i < selectABuf.Length; i++) bufList[i] = (Buf)selectABuf[i].Clone();
        }
        else if (index == 1)
        {
            bufList = new Buf[selectBBuf.Length];
            for (int i = 0; i < selectBBuf.Length; i++) bufList[i] = (Buf)selectBBuf[i].Clone();
        }
        else
        {
            SelectEventEnd();
            return;
        }
        //플레이어 관련 코드
    }
}
