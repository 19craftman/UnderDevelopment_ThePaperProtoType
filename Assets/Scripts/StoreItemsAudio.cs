using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemsAudio : MonoBehaviour
{
    private AudioManager am;
    private bool playedInstance;
    private string a = "FirstItem";
    private string b = "FirstItemNoEggs";
    private string c = "SecondItem";
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        if(GameState.itemsTaken == 0)
        {
            if(GameState.eggSold)
            {
                am.playDialog(a);
            } else
            {
                am.playDialog(b);
            }
            GameState.itemsTaken++;
            Debug.Log(GameState.itemsTaken);
            Destroy(this);
        } 
        else if(GameState.itemsTaken == 1)
        {
            am.playDialog(c);
            GameState.itemsTaken++;
            Destroy(this);
        }
    }
}
