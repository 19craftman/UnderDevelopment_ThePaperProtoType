using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluckington: MonoBehaviour
{
    private AudioManager am;
    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        if(am.dPlaying == false&& GameState.chickenFed==false)
        {
            am.playDialog("Cluck1");
        }
    }

}
