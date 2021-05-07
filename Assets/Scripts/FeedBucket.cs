using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBucket : MonoBehaviour
{
    //this script controls feeding chickens. it is use specifically on the grain bucket. it takes not if the bucket is being dragged, if it is colliding with Cluckington
    //it updates the gameState to denote that cluckington has been fed if the conditions are met
    //conditions: object is being dragged and object is colliding with Cluckington

    [SerializeField] private GameObject cluckington;

    public bool overCluckington;
    private bool overChicken;
    private GameObject chicken;
    private bool dragging;
    private AudioManager am;
    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        overCluckington = false;
        overChicken = false;
        dragging = false;
    }
    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Chicken"))
        {
            overChicken = true;
            chicken = collision.gameObject;
        }
        else if (collision.gameObject.Equals(cluckington))
        {
            overCluckington = true;
        } 

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (overCluckington && collision.gameObject.Equals(cluckington))
        {
            overCluckington = false;
        }
        if (overChicken && collision.gameObject.CompareTag("Chicken"))
        {
            overChicken = false;
            chicken = null;
        }
    }
    private void OnMouseUp()
    {
        if (overCluckington && dragging && !GameState.cluckingtonFed)
        {
            am.playDialog("CluckFed");
            GameState.cluckingtonFed = true;
            dragging = false;
        }
        else if (overChicken && dragging && chicken!=null)
        {
            if(GameState.chickenFed==0)
            {
                am.playDialog("Feed1");
            }
            Transform a = chicken.transform.GetChild(0);
            a.parent = GameObject.FindGameObjectWithTag("Game").transform;
            if (a.name.Equals("Gear"))
            {
                a.localScale = Vector3.one * 1.25f;
            } else
            {
                a.localScale = Vector3.one;
            }
            
            a.gameObject.SetActive(true);

            a.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(destroyChicken(chicken));
            chicken = null;

            GameState.chickenFed++;
            if(GameState.chickenFed==5)
            {
                am.playDialog("Feedlast");
            }
        }
        else if (dragging)
        {
            dragging = false;
        }
    }

    IEnumerator destroyChicken(GameObject c)
    {
        c.transform.DetachChildren();
        c.tag = "Untagged";
        Destroy(c.GetComponent<ClickChicken>());
        c.GetComponent<Animator>().SetTrigger("walkout");
        Vector2 startPos = c.transform.position;
        Vector2 endPos = new Vector2(-14.9f, -23.6f);
        float elapsedTime = 0f;
        float moveTime = .5f*Vector2.Distance(endPos,startPos);
        while (elapsedTime < moveTime)
        {
            c.transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(c);
    }
}
