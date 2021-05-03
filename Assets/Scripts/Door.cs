using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioManager am;
    [SerializeField] private string[] dialog;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private int numClicks;
    bool canClick = true;
    // Start is called before the first frame update
    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
        sr = GetComponent<SpriteRenderer>();
        numClicks = 0;
    }

    private void OnMouseDown()
    {
        if(GameState.puzzleOneSolved && GameState.puzzleTwoSolved && canClick)
        {
            sr.sprite = sprites[numClicks];
            StartCoroutine(playSound(dialog[numClicks]));
            numClicks++;
        }
    }

    IEnumerator playSound(string i)
    {
        canClick = false;
        Sound s = am.soundLookUp(i);
        am.playDialog(i);
        while(s.played == false)
        {
            yield return null;
        }
        if(numClicks>=3)
        {
            //end game here
        }
        canClick = true;
    }
}
