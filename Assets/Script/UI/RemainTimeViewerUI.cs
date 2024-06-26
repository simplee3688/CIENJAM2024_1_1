using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemainTimeViewerUI : MonoBehaviour
{
    TextMeshProUGUI remainTimeText;

    private void Start()
    {
        remainTimeText = GetComponentInChildren<TextMeshProUGUI>();     //�ڽ� ������Ʈ���� ù��° text��ȯ
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        float remainTime = GameManager.Instance.timeGetAdapter.getRemainTime();
        //float remainTime = 10f;
        remainTimeText.text = (Mathf.Floor(remainTime * 10f) / 10f).ToString() + " sec";
    }
}
