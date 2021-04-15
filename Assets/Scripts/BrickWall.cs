using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    //[SerializeField] private Sprite broken;
    private bool colliding;
    private bool dragging;
    private AudioManager am;
    private bool enterRoomPlayed;
    private Inventory inven;
    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        enterRoomPlayed = false;
        colliding = false;
        dragging = false;
        inven = GetComponent<Inventory>();
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

    private void LateUpdate()
    {
        if (!enterRoomPlayed)
        {
            if (!inven.inInven && transform.position.x > 26.5f && transform.position.y > -9.4f)
            {
                am.playDialog("DoorRoomT");
                enterRoomPlayed = true;
            }
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
            am.playDialog("DoorRoomBreak");
            GameState.puzzleTwoSolved = true;
        }
        else if (dragging)
        {
            dragging = false;
        }
    }
}
