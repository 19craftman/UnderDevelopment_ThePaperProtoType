using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    bool over;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(Piano.pianoKeys.Count);
    //    Debug.Log(Piano.correct);
    //    if(collision.gameObject.CompareTag("Grain") && Piano.pianoKeys.Count<2 && !Piano.correct)
    //    {
    //        Piano.pianoKeys.Add(clip);
    //    }
    //}

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            addKey();
        }
    }

    private void addKey()
    {
        GameObject[] grain = GameObject.FindGameObjectsWithTag("Grain");
        bool touching = false;
        for (int i = 0; i < grain.Length; i++)
        {
            if (GetComponent<Collider2D>().IsTouching(grain[i].GetComponent<Collider2D>()))
            {
                touching = true;
                grain[i].GetComponent<Collider2D>().enabled = false;
                break;
            }
        }
        if (touching && Piano.pianoKeys.Count < 3 && !Piano.correct)
        {
            Piano.pianoKeys.Add(clip);
        }
    }
}
