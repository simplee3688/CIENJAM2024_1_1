using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufItem : Item
{
    protected override void GetItemEvent()
    {
        base.GetItemEvent();
        GameManager.Instance.BufItemEvent();
    }
}
