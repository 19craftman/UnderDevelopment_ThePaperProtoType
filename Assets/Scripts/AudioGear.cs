using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGear : MonoBehaviour
{
    private AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.sunPlaced)
        {
            Destroy(this);
        }
    }

    private void OnMouseDown()
    {
        if(!GameState.sunPlaced)
        {
            am.playDialog("GearMissing");
        }
    }
}
