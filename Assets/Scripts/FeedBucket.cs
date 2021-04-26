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
        else if (overChicken && dragging)
        {
            if(GameState.chickenFed==0)
            {
                am.playDialog("Feed1");
            }
            Transform a = chicken.transform.GetChild(0);
            //Debug.Log(a.name);
            a.localScale = Vector3.one;
            a.gameObject.SetActive(true);

            a.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            //Debug.Log(a.gameObject.activeInHierarchy);
            chicken.transform.DetachChildren();
            Destroy(chicken);
            GameState.chickenFed++;
            if(GameState.chickenFed==5)
            {
                am.playDialog("Feedlast");
            }
        
            //Debug.Log("bottom");
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
