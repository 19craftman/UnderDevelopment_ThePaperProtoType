using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSign : MonoBehaviour
{
    private AudioManager am;
    private Sound firstClick;
    private Sound subsequentClicks;
    private int numClicks = 0;
    private bool canClick = true;


    [SerializeField] private GameObject obstacles;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
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
        }
        canClick = true;
    }
}
