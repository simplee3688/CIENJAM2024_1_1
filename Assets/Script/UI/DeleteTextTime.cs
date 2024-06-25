using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class DeleteTextTime : MonoBehaviour
{
    Vector2 position;
    public void setTimer(float duration)
    {
        StartCoroutine(Timer(duration));
    }

    private IEnumerator Timer(float duration)
    {
        
        yield return new WaitForSeconds(duration);

        
        Destroy(this.gameObject);
    }

}
