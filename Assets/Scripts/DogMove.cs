using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    private bool colliding, eating;
    [SerializeField] private float moveTime, eatTime;
    [SerializeField] private Vector2 offset;

    [SerializeField] private GameObject dog;
    private GameObject dogFood;

    private SpriteRenderer spBowl, spDog;
    [SerializeField] private Sprite empty, full;

    private Vector2 startPos, endPos;
    
    // Start is called before the first frame update
    private void Awake()
    {
        colliding = false;
        eating = false;
    }

    private void Start()
    {
        spDog = dog.GetComponent<SpriteRenderer>();
        spBowl = GetComponent<SpriteRenderer>();
        startPos = dog.transform.position;
        endPos = (Vector2)transform.position + offset;
    }

    private void Update()
    {
        if (!eating && colliding)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SpriteRenderer sp = GetComponent<SpriteRenderer>();
                sp.sprite = full;
                if (dogFood != null)
                {
                    Destroy(dogFood);
                    colliding = false;
                    StartCoroutine(goEat());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DogFood"))
        {
            dogFood = collision.gameObject;
            colliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding && collision.gameObject.Equals(dogFood))
        {
            dogFood = null;
            colliding = false;
        }
    }
    IEnumerator goEat()
    {
        eating = true;
        yield return StartCoroutine(moveDog(startPos, endPos));
        //play eat animation
        yield return new WaitForSeconds(eatTime);

        GetComponent<SpriteRenderer>().sprite = empty;

        //stop eat animation
        spDog.flipX = true;
        yield return StartCoroutine(moveDog(endPos, startPos));
        spDog.flipX = false;
        eating = false;
    }
    IEnumerator moveDog(Vector2 s, Vector2 e)
    {
        float elapsedTime = 0f;
        //play walk animation
        while (elapsedTime < moveTime)
        {
            dog.transform.position = Vector2.Lerp(s, e, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dog.transform.position = e;
        //stop walk animation
    }
}
