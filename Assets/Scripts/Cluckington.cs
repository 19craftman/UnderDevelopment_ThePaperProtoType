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
        if(am.dPlaying == false&& GameState.cluckingtonFed==false)
        {
            am.playDialog("Cluck1");
        }
    }

}
