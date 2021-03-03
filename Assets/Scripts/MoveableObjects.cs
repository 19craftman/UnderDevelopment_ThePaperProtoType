using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 mpos;    public Camera cam;    private void OnMouseDrag()    {        transform.position = mpos;    }

    // Start is called before the first frame update
    void Start()
    {
        //Offset = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + 10f);
    }

    // Update is called once per frame
    void Update()
    {
        mpos = Input.mousePosition;        mpos = cam.ScreenToWorldPoint(mpos);        mpos.z = 0;

    }
}
