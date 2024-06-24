using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager
{
    public static void InstantiateTextItemUI(GameObject uiPrefab, TextItem textItem, Transform uiParent, Vector2 position)
    {
        GameObject uiInstance = Object.Instantiate(uiPrefab, uiParent);
        uiInstance.GetComponent<TextMeshProUGUI>().text = textItem.Text;
        uiInstance.GetComponent<DeleteTextTime>().setTimer(textItem.duration);
        // RectTransform을 가져와 위치와 크기를 설정
        RectTransform rectTransform = uiInstance.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchorMin = position;  // 원하는 위치로 설정
            rectTransform.anchorMax = position;
            //rectTransform.sizeDelta = new Vector2(200, 100);  // 원하는 크기로 설정
        }
    }
}