using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 mpos;
    private Camera cam;
    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        bc = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // worldPoint.z = Camera.main.transform.position.z;
            //// Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            //// RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);
            //if()

            mpos = Input.mousePosition;
            mpos = cam.ScreenToWorldPoint(mpos);
            mpos.z = 0;

            if (bc.OverlapPoint(mpos))
            {
                StartCoroutine(drag());
            }
        }
    }

    IEnumerator drag()
    {
        
        while (Input.GetMouseButton(0))
        {
            mpos = Input.mousePosition;
            mpos = cam.ScreenToWorldPoint(mpos);
            mpos.z = 0;
            transform.position = mpos;
            yield return null;
        }
    }
}
