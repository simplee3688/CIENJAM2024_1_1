using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class TextItem : Item
{
    [SerializeField] string text;
    public string Text => text;
    public float duration => regenCoolTime;

    private bool isActived = false;

    public override void GetItemEvent()
    {
        if (!isActived)
        {
            GameManager.Instance.TextItemEvent(this, true);
        }
        isActived = true;
        

    }
    public void ExitItemEvent()
    {
        isActived = false;
        GameManager.Instance.TextItemEvent(this, false);

    }


}
