using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    private bool state = true;
    [SerializeField]
    GameObject story1;
    [SerializeField]
    GameObject story2;
    [SerializeField]
    GameObject story3;

    [SerializeField]
    float changeStoryTime;

    public void SceneChange()
    {
        if (state)
        {
            StartCoroutine(ImageChanger());
            state = false;
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    
    IEnumerator ImageChanger()
    {
        
        story1.SetActive(true);
        yield return new WaitForSeconds(changeStoryTime);
        story2.SetActive(true);
        yield return new WaitForSeconds(changeStoryTime);
        story3.SetActive(true);
        yield return new WaitForSeconds(changeStoryTime);

        SceneManager.LoadScene("GameScene");
    }
    
}
