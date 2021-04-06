using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBucket : MonoBehaviour
{
    [SerializeField] private GameObject cluckington;
    

    public bool colliding;
    private bool dragging;
    // Start is called before the first frame update
    private void Awake()
    {
        colliding = false;
        dragging = false;
    }
    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.Equals(cluckington))
        {
            Debug.Log(1);
            colliding = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(cluckington))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if (colliding && dragging)
        {
            GameState.chickenFed = true;
            Destroy(gameObject);
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
