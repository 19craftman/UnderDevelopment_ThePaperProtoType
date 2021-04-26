using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : MonoBehaviour
{
    private bool dragging;
    private bool colliding;
    private Sprite full;
    private Sprite empty;
    [SerializeField] private GameObject bowl;
    private AudioManager am;
    public Sound clip;

    private void Start()
    {
        dragging = false;
        colliding = false;
        am = FindObjectOfType<AudioManager>();

       // clip = am.soundLookUp("Name");
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(bowl))
        {
            colliding = true;
        }
    }


    private void OnMouseUp()
    {
        if(dragging && colliding)
        {
           // am.playDialog(clip.name);
           
        }
    }

    //while(!clip.played) {
    //    yield return null;
    //    }
}
