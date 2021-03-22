using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSun : MonoBehaviour
{
    bool colliding;
    bool dragging;
    [SerializeField] private GameObject goal;
    // Start is called before the first frame update
    private void Awake()
    {
        colliding = false;
        dragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.Equals(goal))
        {
            colliding = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(colliding && collision.gameObject.Equals(goal))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if(colliding && dragging)
        {
            SpriteRenderer sp = goal.GetComponent<SpriteRenderer>();
            sp.color = new Color(0, 0, 0, 1);
            ChangeDayNight.cogInPlace = true;
            Destroy(gameObject);
        } 
        else if(dragging)
        {
            dragging = false;
        }
    }
}
