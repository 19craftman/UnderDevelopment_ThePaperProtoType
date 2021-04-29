using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brittleHammer : MonoBehaviour
{
    private AudioManager am;
    private Sound click;
    [SerializeField] private GameObject wall;
    private bool colliding, dragging;
    private void Start()
    {
        colliding = false;
        dragging = false;
        am = FindObjectOfType<AudioManager>();
        click = am.soundLookUp("bhClicked");
    }
    private void OnMouseDown()
    {
        if(click.played == false)
        {
            am.playDialog(click.name);
        }
    }
    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.Equals(wall))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(wall))
        {
            colliding = false;
        }
    }

    private void OnMouseUp()
    {
        if(colliding && dragging)
        {
            dragging = false;
            am.playDialog("bhWall");
            Destroy(gameObject);
        }
    }
}
