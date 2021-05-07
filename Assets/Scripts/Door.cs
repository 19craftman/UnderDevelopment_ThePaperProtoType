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

    [SerializeField] private GameObject[] turnOffGame;
    [SerializeField] private GameObject dev;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject paper;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject credits;
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
            
        }
    }

    IEnumerator playSound(string i)
    {
        canClick = false;
        Sound s = am.soundLookUp(i);
        am.playDialog(i);
        yield return new WaitForSeconds(s.clip.length);

        if(numClicks>=3)
        {
            GameState.endGame = true;
            yield return StartCoroutine(endGame());
            Debug.Log("gg");
            Application.Quit();
        }
        numClicks++;
        canClick = true;
    }

    IEnumerator endGame()
    {
        sr.enabled = false;
        am.playEffect("page");
        foreach(GameObject a in turnOffGame)
        {
            a.SetActive(false);
        }
        dev.SetActive(true);
        yield return new WaitForSeconds(.25f);

        Sound s = am.soundLookUp("lighter");
        am.playEffect(s.name);
        yield return new WaitForSeconds(s.clip.length);

        fire.SetActive(true);
        s = am.soundLookUp("fire");
        am.playEffect(s.name);
        yield return new WaitForSeconds(s.clip.length-1f);

        paper.SetActive(false);
        Destroy(dev);
        title.SetActive(true);
        yield return new WaitForSeconds(.05f);
        Destroy(fire);
        
        yield return new WaitForSeconds(3f);

        s = am.soundLookUp("getOut");
        am.playDialog(s.name);
        yield return new WaitForSeconds(s.clip.length);
    }
}
