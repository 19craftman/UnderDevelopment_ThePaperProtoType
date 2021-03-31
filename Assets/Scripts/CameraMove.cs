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

    private Sound coop;
    private Sound piano;
    private Sound doors;
    private AudioManager am;
    private float timer = 0;

    public Arrow left, right, up, down;
    private Arrow[] arrows;

    void Start()
    {
        arrows = new Arrow[]{left, right, up, down};

        am = FindObjectOfType<AudioManager>();
        coop = am.soundLookUp("ChickenCoop");
        piano = am.soundLookUp("PianoRoom1");

        StartCoroutine(PlayAudioIntro());

        moveY = GetComponent<Camera>().orthographicSize*2f;
        moveX = moveY * 1.8f;
        transform.position = locations;
        moving = false;

        foreach (Arrow a in arrows)
        {
            a.Disable();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!am.dPlaying && !coop.played && transform.position == new Vector3(0, -20, -10))
        {
            am.playDialog("ChickenCoop");
        }
        if(!am.dPlaying && !piano.played && transform.position == new Vector3(18, -10, -10))
        {
            am.playDialog("PianoRoom1");
        }

        if (timer >= 180 && transform.position == new Vector3(18, -10, -10)) 
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

    public void Left()
    {
        if(GameState.doorsSetUp)
        {
            if (locations.x != 0 && !moving)
            {
                locations.x -= moveX;
                StartCoroutine(Move());
            }
        }
        
    }

    public void Right()
    {
        if (GameState.titleScreenComplete)
        {
            if (locations.x == 0 && !moving)
            {
                locations.x += moveX;
                StartCoroutine(Move());
            }
        }
    }

    public void Up()
    {
        if (GameState.doorsSetUp)
        {
            if (locations.y != 0 && !moving)
            {
                locations.y += moveY;
                StartCoroutine(Move());
            }
        }
    }

    public void Down()
    {
        if (GameState.doorsSetUp)
        {
            if (locations.y != -20 && !moving)
            {
                locations.y -= moveY;
                StartCoroutine(Move());
            }
        }
    }


    IEnumerator Move()
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

        if (GameState.doorsSetUp == true) 
        {
            foreach(Arrow a in arrows)
            {
                a.ChangeColor(locations);
            }
        }
        else
        {
            right.ChangeColor(locations);
        }

        moving = false;
    }

    IEnumerator PlayAudioIntro()
    {
        am.playDialog("TitleScreen1");
        Sound s = am.soundLookUp("TitleScreen1");
        while(s.played == false)
        {
            yield return null;
        }
        GameState.titleScreenComplete = true;
        right.Enable();
        while(GameState.doorsSetUp == false)
        {
            yield return null;
        }

        foreach(Arrow a in arrows)
        {
            a.ChangeColor(locations);
        }
    }

}
