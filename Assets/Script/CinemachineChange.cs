using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineChange : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            virtualCamera.Priority = 11;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            virtualCamera.Priority = 10;
        }
    }
}
