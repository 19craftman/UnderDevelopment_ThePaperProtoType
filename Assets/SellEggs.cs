using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellEggs : MonoBehaviour
{
    private bool colliding;
    private bool dragging;
    [SerializeField] private GameObject shopClerk;
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
        if (collision.gameObject.Equals(shopClerk))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(shopClerk))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if (dragging && colliding)
        {
            GameState.eggSold = true;
            //play audio here
            Destroy(gameObject);
        }
    }
}
