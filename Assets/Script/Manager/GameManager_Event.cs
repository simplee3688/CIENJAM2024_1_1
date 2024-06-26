using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] GameObject TextUIPrefab;
    [SerializeField] RectTransform canvasRectTransform;

    [SerializeField] ABSelectUI ABselectManager;
    [SerializeField] SelectManager selectManager;

    private Dictionary<string, GameObject> TextItemUI = new Dictionary<string, GameObject>();

    public Tuple<bool, int> chooseEventFlag {  get; private set; }

    public void TextItemEvent(TextItem textItem, bool active)
    {
        if (!TextItemUI.ContainsKey(textItem.Text))
        {
            GameObject obj = UIManager.InstantiateTextItemUI(TextUIPrefab, textItem, canvasRectTransform);
            TextItemUI.Add(textItem.Text, obj);
            obj.SetActive(false);
            return;
        }

        if (active)
        {
            TextItemUI[textItem.Text].SetActive(true);
        }
        else
        {
            GameObject obj;
            bool haveObj = TextItemUI.TryGetValue(textItem.Text, out obj);
            if(haveObj)
            {
                TextItemUI[textItem.Text].SetActive(false);
            }
        }
    }
    
    public void StartEvent()
    {
        Time.timeScale = 0;
        chooseEventFlag = new Tuple<bool, int>(true, 0);
    }
    public void EndEvent(int index)
    {
        Time.timeScale = 1;
        chooseEventFlag = new Tuple<bool, int>(false, index);
    }
    public void SelectEvent()
    {
        selectManager.SelectEventStart();
        
    }
    public void ABSelectEvent(string stra, string strb, Buf[] bufa, Buf[] bufb)
    {
        ABselectManager.SetABselect(stra, strb, bufa, bufb);
        ABselectManager.SelectEventStart();
    }
    private void ChangeTimeRunning(bool run)
    {
        if (run) Time.timeScale = 1.0f;
        else Time.timeScale = 0.0f;
    }

    
}
