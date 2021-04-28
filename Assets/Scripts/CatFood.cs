using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : MonoBehaviour
{
    private bool dragging, colliding;
    [SerializeField] private Sprite full, empty;
    private SpriteRenderer sr;
    [SerializeField] private GameObject bowl, dog;
    private AudioManager am;
    private Sound[] dogAudio, devAudio;
    private int count;
    private Vector3 startPos;

    private void Start()
    {
        dragging = false;
        colliding = false;
        count = 0;
        am = FindObjectOfType<AudioManager>();
        dogAudio = new Sound[] { am.soundLookUp("CFWill1"), am.soundLookUp("CFWill2"), am.soundLookUp("CFWill3") };
        devAudio = new Sound[] { am.soundLookUp("CFRyan1"), am.soundLookUp("CFRyan2"), am.soundLookUp("CFRyan3") };

        sr = bowl.GetComponent<SpriteRenderer>();
        startPos = gameObject.transform.position;
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in collision");
        if (collision.gameObject.Equals(bowl))
        {
            Debug.Log("with bowl");
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(bowl))
        {
            colliding = false;
        }
    }


    private void OnMouseUp()
    {
        if(dragging && colliding)
        {
            Debug.Log(1);
            StartCoroutine(waitForDialog());
           
        }
        dragging = false;
    }

    IEnumerator waitForDialog()
    {
        gameObject.transform.position = startPos;
        sr.sprite = full;
        am.playDialog(dogAudio[count].name);
        yield return new WaitForSeconds(dogAudio[count].clip.length);
        if (count == 2)
        {
            GameState.puzzleOneSolved = true;
            Destroy(dog);
        }

        am.playDialog(devAudio[count].name);

        if (count < 2)
        {
            yield return new WaitForSeconds(devAudio[count].clip.length);
            count++;
            sr.sprite = empty;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
