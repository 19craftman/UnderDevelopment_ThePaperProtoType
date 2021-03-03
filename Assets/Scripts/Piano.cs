using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public static List<AudioClip> pianoKeys = new List<AudioClip>();
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] correctClip;
    public static bool correct=false;
    private void LateUpdate()
    {
        if(pianoKeys.Count>=2)
        {
            Debug.Log("here");
            AudioClip[] ac = pianoKeys.ToArray();
            pianoKeys.Clear();
            if(correct)
            {
                StartCoroutine(playNotes(ac));
            }
            
        }
    }


    IEnumerator playNotes(AudioClip[] ac)
    {
        //disable clicking
        int i = 0;
        while(i<3)
        {
            if (!source.isPlaying)
            {
                correct = correctClip[i].Equals(ac[i]);
                source.PlayOneShot(ac[i]);
                i++;
                yield return null;
            }
        }
        if (correct)
        {
            //give cue that grain are gone.
        }
        //enable clicking

    }

}
