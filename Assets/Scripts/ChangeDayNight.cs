using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDayNight : MonoBehaviour
{
    public static bool cogInPlace;
    public static bool day;
    public GameObject[] dayRooms;
    public GameObject[] nightRooms;

    public Sprite dayS, night;

    private void Awake()
    {
        cogInPlace = false;
        day = false;
    }

    private void OnMouseDown()
    {
        if(cogInPlace)
        {
            if(day)
            {
                swap(nightRooms, dayRooms);
                GetComponent<SpriteRenderer>().sprite = night;
            }
            else
            {
                swap(dayRooms, nightRooms);
                GetComponent<SpriteRenderer>().sprite = dayS;
            }
            day = !day;
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
