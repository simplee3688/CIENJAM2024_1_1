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

    public int selectIndex { get; private set; }

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
        GameManager.Instance.StartEvent();
        gameObject.SetActive(true);
    }

    public void SelectEventEnd(int index)
    {
        GameManager.Instance.EndEvent(index);
        gameObject.SetActive(false);
    }
    public void SelectOption(int index)
    {
        Debug.Log(index + " : select");
        selectIndex = index;
        if(true)    //조건
        {
            buttons[index].SetActive(false);

            //cost 지불
            SelectEventEnd(index);
        }


    }
}
