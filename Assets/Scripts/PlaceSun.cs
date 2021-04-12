using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSun : MonoBehaviour
{
    //this script controls items that can be used to complete the clock puzzle(gear or sun). it takes note of if they are being dragged, if they are colliding with goal
    //and updates the gamestate, goal sprite, and plays audio if the conditions are met.
    //conditions: object is being dragged and object is colliding with the goal object

    private bool colliding;
    private bool dragging;
    [SerializeField] private bool isSun;
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
            sp.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            sp.color = new Color(0, 0, 0, 1);

            if(isSun)
            {
                //play bad audio
            } else
            {
                //play good audio
            }
            GameState.sunPlaced = true;

            

            Destroy(gameObject);
        } 
        else if(dragging)
        {
            dragging = false;
        }
    }
}
