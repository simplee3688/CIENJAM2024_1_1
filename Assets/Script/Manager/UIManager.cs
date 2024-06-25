using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager
{
    public static GameObject InstantiateTextItemUI(GameObject uiPrefab, TextItem textItem, Transform uiParent)
    {
        GameObject uiInstance = Object.Instantiate(uiPrefab, uiParent);
        uiInstance.GetComponent<TextMeshProUGUI>().text = textItem.Text;
        
        return uiInstance;
        // RectTransform�� ������ ��ġ�� ũ�⸦ ����
        
    }
}