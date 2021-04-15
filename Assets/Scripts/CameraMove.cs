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
    private Sound storeOpen;
    private Sound storeClosed;
    private Sound clock, clock2;
    private AudioManager am;
    private float timer = 0;

    public Arrow left, right, up, down;
    private Arrow[] arrows;

    [SerializeField] private GameObject toolBar;//this includes the sun moon and toolbox

    void Start()
    {
        arrows = new Arrow[]{left, right, up, down};

        am = FindObjectOfType<AudioManager>();
        coop = am.soundLookUp("Coop1");
        piano = am.soundLookUp("PianoRoom1");
        storeClosed = am.soundLookUp("StoreClosed");
        storeOpen = am.soundLookUp("StoreOpen");
        clock = am.soundLookUp("Clock1");
        clock2 = am.soundLookUp("Clock2");

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
            am.playDialog(coop.name);
        }
        if (!am.dPlaying && !piano.played && transform.position == new Vector3(18, -10, -10))
        {
            am.playDialog(piano.name);
        }
        if(!am.dPlaying && transform.position == new Vector3(0, -10, -10))
        {
            Debug.Log("store");
            if(!storeClosed.played && !GameState.dayTime)
            {
                Debug.Log("closed");
                am.playDialog(storeClosed.name);
            } else if (!storeOpen.played && GameState.dayTime)
            {
                am.playDialog(storeOpen.name);
            }
        }
        if (transform.position == new Vector3(18, -20, -10))
        {
            if(!am.dPlaying && !clock.played)
            {
                am.playDialog(clock.name);
            }
            //else
            //{
            //    am.playDialog(clock2.name);
            //}
        } 

        //if (timer >= 180 && transform.position == new Vector3(18, -10, -10)) 
        //{
        //    am.playDialog("Hints1");
        //    timer = 0;
        //}
        //else
        //{
        //    timer += Time.deltaTime;
        //}

    }

    public void Left()
    {
        if(GameState.titleScreenComplete)
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
        if (GameState.titleScreenComplete)
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
        if (GameState.titleScreenComplete)
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

        if (GameState.titleScreenComplete == true) 
        {
            foreach(Arrow a in arrows)
            {
                a.ChangeColor(locations);
            }
        }

        moving = false;
    }

    //play the first line, enable the toolBar, play the second line, set up the arrows
    IEnumerator PlayAudioIntro()
    {
        
        am.playDialog("TitleScreen1");
        Sound s = am.soundLookUp("TitleScreen1");
        yield return new WaitForSeconds(s.clip.length);

        toolBar.SetActive(true);
        am.playDialog("TitleScreen2");
        s = am.soundLookUp("TitleScreen2");
        yield return new WaitForSeconds(s.clip.length);

        foreach (Arrow a in arrows)
        {
            a.ChangeColor(locations);
        }
        GameState.titleScreenComplete = true;
    }

}
