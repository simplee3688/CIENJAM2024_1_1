using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] bool[] ability = new bool[] { false, false, false };
    public bool isAllability { get; private set; }
    public void addBufs(Buf buf)
    {
        bufManager.Add(buf);
    }
    public void addBufs(Buf[] bufs)
    {
        for (int i = 0; i < bufs.Length; i++)
        {
            bufManager.Add(bufs[i]);
        }
    }

    public void GetAbility(int index)
    {
        ability[index] = true;
        if(index == 0) //2 jump
        {
            maxJumpCount = 2;
            jumpCount = 2;
        }
        else if(index == 1) //dash
        {
            maxDashCount = 1;
        }
        else                //speed up
        {
            Buf buf = new Buf(BufEnum.speedPercent, 10, 25, true);
            addBufs(buf);
        }
        isAllability = true;
        foreach (bool i in ability)
        {
            isAllability &= i;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("isTrigger");
        if (collision.tag == "Obstacle")
        {
            Debug.Log("It's Obsacle!");
            if (graceTime <= 0)
            {
                var damageInfo = collision.GetComponent<Obstacle>().GetDamageInfo();
                graceTime = damageInfo.gracePeriod;
                foreach (Buf buf in damageInfo.bufList)
                {
                    bufManager.Add(buf);
                }
                StartCoroutine("damageEffect");
            }
        }
        else if (collision.tag == "TextItem")
        {
            collision.GetComponent<TextItem>().GetItemEvent();
        }
        else if (collision.tag == "KeyItem")
        {
            collision.GetComponent<KeyItem>().GetItemEvent();
        }
        else if (collision.tag == "BufItem")
        {
            collision.GetComponent<BufItem>().GetItemEvent(this);   
        }
        else if(collision.tag == "Respawn")
        {
            ResponeArea respawnInfo = collision.GetComponent<ResponeArea>();
            if(respawnInfo != null)
            {
                timeManager.reduceTime(respawnInfo.getCost());
                this.transform.position = respawnInfo.getRespawnPosition();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TextItem") collision.GetComponent<TextItem>().ExitItemEvent();
    }
}
