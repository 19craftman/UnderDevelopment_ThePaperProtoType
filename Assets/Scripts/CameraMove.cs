using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 locations = new Vector3(0,0,-10);
    private float moveby;
    private bool moving;
    private float moveTime = 2f;

    void Start()
    {
        moveby = GetComponent<Camera>().orthographicSize*2f;
        transform.position = locations;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void left()
    {
        if(locations.x !=0 && !moving)
        {
            locations.x -= moveby;
            StartCoroutine(move());
        }
    }

    public void right()
    {
        if (locations.x == 0 && !moving)
        {
            locations.x += moveby;
            StartCoroutine(move());
        }
    }

    public void up()
    {
        if (locations.y!=0 && !moving)
        {
            locations.y += moveby;
            StartCoroutine(move());
        }
    }

    public void down()
    {
        if (locations.y!=-20 && !moving)
        {
            locations.y -= moveby;
            StartCoroutine( move());
        }
    }


    IEnumerator move()
    {
        moving = true;
        //Vector3 startPos = transform.position;
        float elapsedTime = 0;

        while(elapsedTime<moveTime)
        {
            transform.position = Vector3.Lerp(transform.position, locations, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moving = false;
    }

}
