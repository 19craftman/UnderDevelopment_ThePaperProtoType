using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBucket : MonoBehaviour
{
    [SerializeField] private GameObject cluckington;
    private bool colliding;
    private bool dragging;
    private GameObject bucket;
    [SerializeField] private Sprite fullBucket;
    private AudioManager am;
    private static bool lastEgg;
    public bool overCluckington;

    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        colliding = false;
        dragging = false;
        lastEgg= false;
        overCluckington = false;
        bucket = GameObject.FindGameObjectWithTag("Bucket");
    }
    private void OnMouseDrag()
    {
        
        dragging = true;
    }
    private void OnMouseDown()
    {
        if (GameState.eggsCollected>=5 && GetComponent<Inventory>()==null)
        {
            Debug.Log("here");
            gameObject.AddComponent<Inventory>();
            lastEgg = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(bucket))
        {
            colliding = true;
        }
        else if (collision.gameObject.Equals(cluckington))
        {
            overCluckington = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(colliding && collision.gameObject.Equals(bucket))
        {
            colliding = false;
        }
        if (overCluckington && collision.gameObject.Equals(cluckington))
        {
            overCluckington = false;
        }
    }
    private void OnMouseUp()
    {
        if (GameState.eggsCollected < 5)
        {
            if (dragging && colliding)
            {
                GameState.eggsCollected++;
                if (GameState.eggsCollected >= 5)
                {
                    lastEgg = true;
                    am.playDialog("EggsCollected");
                    bucket.GetComponent<SpriteRenderer>().sprite = fullBucket;
                    bucket.AddComponent<MoveableObjects>();
                    
                }
                else
                {
                    //play audio
                   
                }
                Destroy(gameObject);
                }
            }
        if (overCluckington && dragging && gameObject.GetComponent<Inventory>() == true)
        {
            cluckington.transform.GetChild(0).gameObject.SetActive(true);
            am.playDialog("cluckEgg");
            Destroy(gameObject);
            //Put egged cluckinton audio here
        }
    }
  
}
