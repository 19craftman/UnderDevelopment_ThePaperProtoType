using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private AudioClip clip;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(Piano.pianoKeys.Count);
        Debug.Log(Piano.correct);
        if(collision.gameObject.CompareTag("Grain") && Piano.pianoKeys.Count<2 && !Piano.correct)
        {
            Piano.pianoKeys.Add(clip);
        }
    }
}
