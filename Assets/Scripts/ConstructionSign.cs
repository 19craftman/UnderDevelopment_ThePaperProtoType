using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSign : MonoBehaviour
{
    private AudioManager am;
    private int numClicks = 0;
    private bool canClick = true;
    private SpriteRenderer sr;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject obstacles;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(canClick)
        {
            numClicks++;
            StartCoroutine(soundPlaying("DoorRoom" + numClicks));
        }
    }

    IEnumerator soundPlaying(string name)
    {
        canClick = false;
        Sound s = am.soundLookUp(name);
        am.playDialog(name);
        while(s.played == false)
        {
            yield return null;
        }
        if (numClicks == 4)
        {
            GameState.doorsSetUp = true;
            obstacles.SetActive(true);

            am.playDialog("DoorRoom5");
            Destroy(gameObject);
        } else
        {
            //sr.sprite = sprites[numClicks - 1];
            Debug.Log("door room sign sprite change");
        }
        canClick = true;
    }
}
