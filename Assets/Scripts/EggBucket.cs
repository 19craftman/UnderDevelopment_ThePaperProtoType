using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBucket : MonoBehaviour
{
    private bool colliding;
    private bool dragging;
    private GameObject bucket;
    [SerializeField] private Sprite fullBucket;
    private AudioManager am;
    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        colliding = false;
        dragging = false;
        bucket = GameObject.FindGameObjectWithTag("Bucket");
    }
    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(bucket))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(colliding && collision.gameObject.Equals(bucket))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if(dragging && colliding)
        {
            GameState.eggsCollected++;
            if(GameState.eggsCollected==5)
            {
                am.playDialog("EggsCollected");
                bucket.GetComponent<SpriteRenderer>().sprite = fullBucket;
                bucket.AddComponent<MoveableObjects>();
            } else
            {
                //play audio
            }
            Destroy(gameObject);
        }
    }
}
