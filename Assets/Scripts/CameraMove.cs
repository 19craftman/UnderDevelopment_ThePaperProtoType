using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 locations = new Vector3(0,0,-10);
    private float moveY;
    private float moveX;
    private bool moving;
    private float moveTime = .5f;
    Sound coop;
    Sound piano;
    AudioManager am;
    private float timer = 0;
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        coop = am.soundLookUp("ChickenCoop");
        piano = am.soundLookUp("PianoRoom1");

        moveY = GetComponent<Camera>().orthographicSize*2f;
        moveX = moveY * 1.8f;
        transform.position = locations;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!am.dPlaying && !coop.played && transform.position == new Vector3(0, -10, -10))
        {
            am.playDialog("ChickenCoop");
        }
        if(!am.dPlaying && !piano.played && transform.position == new Vector3(18, -10, -10))
        {
            am.playDialog("PianoRoom1");
        }

        if (timer >= 180 && transform.position == new Vector3(10, -10, -10)) 
        {
            am.playDialog("Hints1");
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        //Debug.Log(timer);

    }

    public void left()
    {
        if(locations.x !=0 && !moving)
        {
            locations.x -= moveX;
            StartCoroutine(move());
        }
    }

    public void right()
    {
        if (locations.x == 0 && !moving)
        {
            locations.x += moveX;
            StartCoroutine(move());
        }
    }

    public void up()
    {
        if (locations.y!=0 && !moving)
        {
            locations.y += moveY;
            StartCoroutine(move());
        }
    }

    public void down()
    {
        if (locations.y!=-20 && !moving)
        {
            locations.y -= moveY;
            StartCoroutine( move());
        }
    }


    IEnumerator move()
    {
        moving = true;
        //Vector3 startPos = transform.position;
        float elapsedTime = 0;

        while(elapsedTime<moveTime)
        {
            transform.position = Vector3.Lerp(transform.position, locations, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = locations;
        moving = false;
    }

}
