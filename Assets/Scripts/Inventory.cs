using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject inventory;
    private bool colliding;
    private bool dragging;
    // Start is called before the first frame update
    private void Awake()
    {
        colliding = false;
        dragging = false;
    }
    private void OnMouseDrag()
    {
        if(transform.parent!=null)
        {
            if (transform.parent.gameObject.CompareTag("Inventory"))
            {
                transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

            transform.parent = null;
        }
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inventory"));
        {
            inventory = collision.gameObject;
            colliding = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(inventory))
        {
            colliding = false;
            inventory = null;
        }
    }
    private void OnMouseUp()
    {
        if (colliding && dragging)
        {
            transform.parent = inventory.transform;
            transform.localPosition = Vector3.zero;
            inventory.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
