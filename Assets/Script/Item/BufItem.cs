using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufItem : Item
{
    [SerializeField] Buf[] bufs;
    public virtual void GetItemEvent(Player player)
    {
        base.GetItemEvent();
        
        player.addBufs(getBufs(bufs));
    }

    protected Buf[] getBufs(Buf[] bufs)
    {
        Buf[] bufs2 = new Buf[bufs.Length];
        for (int i = 0; i < bufs.Length; i++)
        {
            bufs2[i] = (Buf)bufs[i].Clone();
        }
        return bufs2;
    }
}
