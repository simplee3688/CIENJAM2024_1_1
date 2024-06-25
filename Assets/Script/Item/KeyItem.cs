using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    [SerializeField] int key;
    public override void GetItemEvent()
    {
        
        Tile_BreakWithKey.GetKey(key);
        Destroy(this.gameObject);
    }
}
