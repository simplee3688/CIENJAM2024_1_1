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
        // RectTransform�� ������ ��ġ�� ũ�⸦ ����
        RectTransform rectTransform = uiInstance.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchorMin = position;  // ���ϴ� ��ġ�� ����
            rectTransform.anchorMax = position;
            //rectTransform.sizeDelta = new Vector2(200, 100);  // ���ϴ� ũ��� ����
        }
    }
}