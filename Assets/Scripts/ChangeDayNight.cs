using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDayNight : MonoBehaviour
{
    public GameObject[] dayRooms;
    public GameObject[] nightRooms;

    public Sprite dayS, night;

    private void Awake()
    {
    }

    private void OnMouseDown()
    {
        if(GameState.sunPlaced)
        {
            if(GameState.dayTime)
            {
                swap(nightRooms, dayRooms);
                GetComponent<SpriteRenderer>().sprite = night;
            }
            else
            {
                swap(dayRooms, nightRooms);
                GetComponent<SpriteRenderer>().sprite = dayS;
            }
            GameState.dayTime = !GameState.dayTime;
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
