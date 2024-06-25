using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufItem : Item
{
    public override void GetItemEvent()
    {
        base.GetItemEvent();
        GameManager.Instance.BufItemEvent();
    }
}
