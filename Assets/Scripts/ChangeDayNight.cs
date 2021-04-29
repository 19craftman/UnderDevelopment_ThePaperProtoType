using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDayNight : MonoBehaviour
{
    public GameObject[] dayRooms;
    public GameObject[] nightRooms;

    private AudioManager am;
    [SerializeField] private Animator animGear;
    [SerializeField] private Animator animClock;

    public Sprite dayS, night;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        if(GameState.sunPlaced)
        {
            animClock.SetTrigger("clockHands");
            if(GameState.dayTime)
            {
                swap(nightRooms, dayRooms);
                GetComponent<SpriteRenderer>().sprite = night;
                am.playDialog("Night");
            }
            else
            {
                swap(dayRooms, nightRooms);
                GetComponent<SpriteRenderer>().sprite = dayS;
                am.playDialog("Day");
            }
            GameState.dayTime = !GameState.dayTime;
        } else
        {
            am.playDialog("LeverBroken");
            animGear.SetTrigger("broken");
        }
    }

    private void swap(GameObject[] on, GameObject[] off)
    {
        foreach(GameObject a in on)
        {
            a.SetActive(true);
        }
        foreach(GameObject b in off)
        {
            b.SetActive(false);
        }
    }


}
