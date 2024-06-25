using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : BufItem
{
    
    public override void GetItemEvent(Player player)
    {
        base.GetItemEvent();
        Debug.Log("done");
        if(!player.isAllability) StartCoroutine(ChooseEvent(player));
    }

    IEnumerator ChooseEvent(Player player)
    {
        GameManager.Instance.SelectEvent();
        yield return new WaitUntil(() => GameManager.Instance.chooseEventFlag.Item1 == false);
        player.GetAbility(GameManager.Instance.chooseEventFlag.Item2);
    }   

}
