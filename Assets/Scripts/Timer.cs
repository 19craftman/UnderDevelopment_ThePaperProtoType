using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 180 ) // Plus In room
        {
            //Play Audio Here
            timer = 0;
        }
        
        else
        {
            timer += Time.deltaTime;
        }
 
        Debug.Log(timer);
    }
}
