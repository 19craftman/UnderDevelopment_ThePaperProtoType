using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public static List<AudioClip> pianoKeys = new List<AudioClip>();
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] correctClip;
    public static bool correct=false;

    private AudioManager am;
    private string[] cluckSounds = new string[] { "Cluck2", "Cluck3", "Cluck4" };
    private int index;

    [SerializeField] private GameObject toActivate;

    private bool playing;

    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
        index = 0;
    }
    public AudioClip[] ac;
    private void LateUpdate()
    {
        if(pianoKeys.Count>=3)
        {
            ac = pianoKeys.ToArray();
            pianoKeys.Clear();
            if(!correct)
            {
                StartCoroutine(playNotes(ac));
            }
            
        }
    }

    private void OnMouseDown()
    {
        if(GameState.chickenFed)
        {
            if(!playing)
            {
                toActivate.SetActive(!toActivate.activeInHierarchy);
            }
        }
        else
        {
            am.playDialog(cluckSounds[index]);
            index = (index + 1) % 3;
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
        if (correct)
        {
            Destroy(toActivate);
            Destroy(this);
        }

        playing = false; //enable clicking

    }

}
