using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextItem : Item
{
    [SerializeField] string text;
    public string Text => text;
    public float duration => regenCoolTime;

    public void Start()
    {
        GetItemEvent();
    }

    public override void GetItemEvent()
    {
        base.GetItemEvent();
        GameManager.Instance.TextItemEvent(this);
    }
    
}
