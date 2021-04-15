using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnClick : MonoBehaviour
{
    [SerializeField] private string soundName;
    private AudioManager am;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }
    private void OnMouseDown()
    {
        if(!am.dPlaying && GameState.titleScreenComplete)
        {
            am.playDialog(soundName);
        }
    }
}
