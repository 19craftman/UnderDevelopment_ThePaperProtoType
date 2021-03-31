using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioManager am;
    private Sound firstClick;
    private Sound subsequentClicks;
    private int numClicks = 0;
    private bool canClick = true;


    [SerializeField] private GameObject[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        firstClick = am.soundLookUp("DoorRoom1");
        subsequentClicks = am.soundLookUp("DoorRoom2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(canClick)
        {
            numClicks++;
            if (numClicks == 1)
            {
                StartCoroutine(soundPlaying(firstClick));
            } else if (numClicks >=2)
            {
                StartCoroutine(soundPlaying(subsequentClicks));
            }
        }
    }

    IEnumerator soundPlaying(Sound s)
    {
        canClick = false;
        am.playDialog(s.name);
        while(s.played == false)
        {
            yield return null;
        }
        if (GameState.doorsSetUp == false)
        {
            GameState.doorsSetUp = true;
            foreach(GameObject a in obstacles)
            {
                a.SetActive(true);
            }
        }
        canClick = true;
    }
}
