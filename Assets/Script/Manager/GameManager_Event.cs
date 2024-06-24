using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] GameObject TextUIPrefab;
    [SerializeField] RectTransform canvasRectTransform;

    public void BufItemEvent()
    {
        
    }

    public void TextItemEvent(TextItem textItem)
    {
        Vector2 pos = textItem.transform.position;
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);
        UIManager.InstantiateTextItemUI(TextUIPrefab, textItem, canvasRectTransform, viewportPoint);
    }

    private void ChangeTimeRunning(bool run)
    {
        if (run) Time.timeScale = 1.0f;
        else Time.timeScale = 0.0f;
    }

    
}
