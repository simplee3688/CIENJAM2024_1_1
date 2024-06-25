using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABSelectUI : MonoBehaviour
{
    [SerializeField]
    Image AImage, BImage;
    
    string Acomment, Bcomment;

    [SerializeField]
    Buf[] selectABuf, selectBBuf;
    
    public void SetABselect(string Acomment, string Bcomment, Buf[] selectABuf, Buf[] selectBBuf, Image AImage = null, Image BImage = null)
    {
        this.Acomment = Acomment;
        this.Bcomment = Bcomment;
        this.selectABuf = selectABuf;
        this.selectBBuf = selectBBuf;
        if(AImage != null && BImage != null)
        {
            this .AImage = AImage;
            this .BImage = BImage;
        }
    }
    public void SelectEventStart()
    {
        GameManager.Instance.StartEvent();

        gameObject.SetActive(true);
    }

    public void SelectEventEnd(int index)
    {
        GameManager.Instance.EndEvent(index);

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
            index = 3;
            return;
        }

        SelectEventEnd(index);
        //플레이어 관련 코드
    }
}
