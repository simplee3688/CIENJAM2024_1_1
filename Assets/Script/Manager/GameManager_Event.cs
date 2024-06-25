using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] GameObject TextUIPrefab;
    [SerializeField] RectTransform canvasRectTransform;

    private Dictionary<string, GameObject> TextItemUI = new Dictionary<string, GameObject>();

    public void BufItemEvent()
    {
        
    }

    public void TextItemEvent(TextItem textItem, bool active)
    {
        if(active)
        {
            if(!TextItemUI.ContainsKey(textItem.Text))
            {
                TextItemUI.Add(textItem.Text, UIManager.InstantiateTextItemUI(TextUIPrefab, textItem, canvasRectTransform));
            }
            
        }
        else
        {
            GameObject obj;
            bool haveObj = TextItemUI.TryGetValue(textItem.Text, out obj);
            if(haveObj)
            {
                TextItemUI.Remove(textItem.Text);
                Destroy(obj);
            }
            
        }
    }
    

    private void ChangeTimeRunning(bool run)
    {
        if (run) Time.timeScale = 1.0f;
        else Time.timeScale = 0.0f;
    }

    
}
