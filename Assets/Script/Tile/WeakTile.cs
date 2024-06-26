using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakTile : MonoBehaviour
{
    [SerializeField] float responeTime;
    [SerializeField] float deleteTime;

    [SerializeField] Tilemap tileMap;
    [SerializeField] CompositeCollider2D comCollider;

    [SerializeField] bool isDelete;
    [SerializeField] bool isThereAnything;

    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
        comCollider = GetComponent<CompositeCollider2D>();
        isDelete = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDelete && collision.gameObject.tag == "Player" && collision.transform.position.y > this.transform.position.y)
        {
            StartCoroutine("startDelete");
        }
    }

    IEnumerator startDelete()
    {
        isDelete = true;
        float currentDeleteTime = deleteTime;
        Color color = tileMap.color;
        while (currentDeleteTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentDeleteTime -= 0.1f;
            color.a = currentDeleteTime / deleteTime;
            tileMap.color = color;
        }
        comCollider.isTrigger = true;
        StartCoroutine("startRespone");
    }

    IEnumerator startRespone()
    {
        yield return new WaitForSeconds(responeTime);
        this.gameObject.SetActive(true);

        while (isThereAnything)
        {
            yield return new WaitForSeconds(0.1f);
        }

        comCollider.isTrigger = false;
        Color color = tileMap.color;
        color.a = 1;
        tileMap.color = color;
        isDelete = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isThereAnything = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isThereAnything = false;
        }
    }
}
