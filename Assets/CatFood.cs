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

    private void Start()
    {
        dragging = false;
        colliding = false;
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
}
