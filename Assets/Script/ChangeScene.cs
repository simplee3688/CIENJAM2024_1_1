using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    private bool state = true;
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    private float time;

    private void Update()
    {
        
    }
    public void SceneChange() {


        while (true)
        {
            if (time > 0.1f)
            {
                story1.SetActive(state);
            }
            if (time > 30.0f)
            {
                story2.SetActive(state);
            }
            if (time > 50.0f)
            {
                story3.SetActive(state);
                break;
            }
        }

 

        SceneManager.LoadScene("ttt");
    }
    // Start is called before the first frame update
    
}
