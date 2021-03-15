using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public static List<AudioClip> pianoKeys = new List<AudioClip>();
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] correctClip;
    public static bool correct=false;

    [SerializeField] private GameObject toActivate;

    private bool playing;


    private void LateUpdate()
    {
        if(pianoKeys.Count>=3)
        {
            AudioClip[] ac = pianoKeys.ToArray();
            pianoKeys.Clear();
            if(!correct)
            {
                StartCoroutine(playNotes(ac));
            }
            
        }
    }

    private void OnMouseDown()
    {
        if(!playing)
        {
            toActivate.SetActive(!toActivate.activeInHierarchy);
        }
    }

    IEnumerator playNotes(AudioClip[] ac)
    {
        playing = true;//disable clicking
        int i = 0;
        while(i<3)
        {
            if(source.isPlaying)
            {
                source.Stop();
            }
                
                source.PlayOneShot(ac[i]);
                i++;
            yield return new WaitForSeconds(4);
        }
        bool temp = true;
        for(int j=0; j < 3; j++)
        {
            if (!ac[j].Equals(correctClip[j]))
            {
                temp = false;
                break;
            }
        }
        correct = temp;
        source.Stop();
        GameObject[] grain = GameObject.FindGameObjectsWithTag("Grain");
        for(int index=0; index<grain.Length; index++)
        {
            Destroy(grain[index]);
        }
        GrainSpawning.numberOfGain.Clear();
        if (correct)
        {
            Destroy(toActivate);
            Destroy(this);
            //give cue that grain are gone.
        }

        playing = false; //enable clicking

    }

}
