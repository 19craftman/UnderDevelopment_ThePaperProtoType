using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosOnClick : MonoBehaviour
{
    private AudioManager am;
    [SerializeField] private string[] lines;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        i = 0;
    }

    private void OnMouseDown()
    {
        if(!am.dPlaying)
        {
            if(i>=lines.Length)
            {
                am.playDialog(lines[lines.Length - 1]);
            } else 
            {
                am.playDialog(lines[i]);
                i++;
            }
        }
    }
}
