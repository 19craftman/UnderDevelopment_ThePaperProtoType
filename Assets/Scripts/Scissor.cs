using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : MonoBehaviour
{
    [SerializeField] private GameObject rope, cam, letter;
    [SerializeField] private Sprite cutRope, cutLetter;
    [SerializeField] private Vector2 dropPos;
    [SerializeField] private float moveTime;

    private Inventory inven;
    private bool dragging, colliding, s1Played;
    private AudioManager am;
    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        inven = GetComponent<Inventory>();
        colliding = false;
        dragging = false;
        s1Played = false;
    }
    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void LateUpdate()
    {
        if(!s1Played)
        {
            if (!inven.inInven && transform.position.x < 8.7f && transform.position.y > 8)
            {
                am.playDialog("TitleScreenScissor");
                s1Played = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.Equals(rope))
        {
            colliding = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(rope))
        {
            colliding = false;
        }
    }
    private void OnMouseUp()
    {
        if (colliding && dragging)
        {
            //rope.GetComponent<SpriteRenderer>().sprite = cutRope;
            //letter.GetComponent<SpriteRenderer>().sprite = cutLetter;
            Destroy(rope);
            am.playDialog("TitleScreenCut");

            letter.GetComponent<Animator>().SetTrigger("go");
            Destroy(gameObject);
        }
        else if (dragging)
        {
            dragging = false;
        }
    }

}
