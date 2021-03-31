using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : MonoBehaviour
{
    [SerializeField] private GameObject rope, cam, letter;
    [SerializeField] private Sprite cutRope, cutLetter;
    [SerializeField] private Vector2 dropPos;
    [SerializeField] private float moveTime;

    public bool colliding;
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
        
        if (collision.gameObject.Equals(rope))
        {
            Debug.Log(1);
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
            StartCoroutine(DropObject());
        }
        else if (dragging)
        {
            dragging = false;
        }
    }

    IEnumerator DropObject()
    {
        float timeElapsed = 0;
        while (timeElapsed < moveTime)
        {
            letter.transform.position = Vector2.Lerp(letter.transform.position, dropPos, timeElapsed / moveTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0;
        Vector3 startPos = cam.transform.position;
        Vector3 shakePos = startPos;
        int count = 0;
        
        while(timeElapsed< moveTime)
        {
            float offsetY = ((moveTime - timeElapsed) / moveTime) * 0.2f;
            
            if (count % 2 != 0)
            {
                offsetY = -offsetY;
            }
            shakePos.y += offsetY;

            cam.transform.position = shakePos;
            timeElapsed += Time.deltaTime;
            count++;
            yield return null;
        }
        cam.transform.position = startPos;
        letter.AddComponent<MoveableObjects>();
        Destroy(gameObject);
    }
}
