using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 mpos;
    private Camera cam;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
      
    }
    private void OnMouseDrag()
    {
        transform.position = mpos;
    }
    // Update is called once per frame
    void Update()
    {
        mpos = Input.mousePosition;
        mpos = cam.ScreenToWorldPoint(mpos);
        mpos.z = 0;

    }
}
