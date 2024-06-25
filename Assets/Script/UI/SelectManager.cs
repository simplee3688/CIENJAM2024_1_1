using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] text;

    private GameObject[] buttons;

    [SerializeField]
    int[] cost;

    public void Start()
    {
        buttons = new GameObject[text.Length];
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = text[i].transform.parent.gameObject;
            text[i].text = "Cost : " + cost[i];
        }    
    }

    public void SelectEventStart()
    {
        gameObject.SetActive(true);
    }

    public void SelectEventEnd()
    {
        gameObject.SetActive(false);
    }
    public void SelectOption(int index)
    {
        Debug.Log(index + " : select");

        if(true)
        {
            buttons[index].SetActive(false);

            //cost 지불
            //플레이어 관련 버프
            SelectEventEnd();
        }


    }
}
