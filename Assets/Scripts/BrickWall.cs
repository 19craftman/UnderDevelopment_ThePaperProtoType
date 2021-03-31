using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    //[SerializeField] private Sprite broken;
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
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(wall))
        {
            colliding = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(wall))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if (colliding && dragging)
        {
            //wall.GetComponent<SpriteRenderer>().sprite = broken;
            Destroy(wall);
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
