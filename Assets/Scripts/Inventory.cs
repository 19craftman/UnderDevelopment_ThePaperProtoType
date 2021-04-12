using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //this script controls items that can be added to inventory. it takes note of if they are being dragged, if they are colliding with inventory
    //and utilizes insideInventory script to add items to the invetory if the conditions are met when the mouse is released.
    //conditions: object is being dragged and object is colliding with the inventory object

    private GameObject inventory;
    private InsideInventory insideInventory;
    private bool colliding;
    private bool dragging;
    public bool inInven;
    // Start is called before the first frame update
    private void Awake()
    {
        inInven = false;
        colliding = false;
        dragging = false;
        insideInventory = FindObjectOfType<InsideInventory>();
        inventory = insideInventory.gameObject;
    }
    private void OnMouseDrag()
    {
        dragging = true;
        if (inInven)
        {
            insideInventory.removeObject(gameObject);
            inInven = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(inventory))
        {
            colliding = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(inventory))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if (colliding && dragging)
        {
            dragging = false;
            inInven = insideInventory.addObject(gameObject);
            if(!inInven)
            {
                //play inventory full clip
            }
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
