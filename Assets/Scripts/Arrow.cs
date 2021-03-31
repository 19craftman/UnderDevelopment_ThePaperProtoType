using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    private SpriteRenderer sr;

    public float position;
    enum Axis { vertical, horizontal};
    [SerializeField] private Axis axis;

    private Color normal;

    private Color faded;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        normal = sr.color;
        faded = normal;
        faded.a = .5f;
    }

    public void ChangeColor(Vector3 currLocation)
    {
        float temp;
        if(axis == Axis.vertical)
        {
            temp = currLocation.y;
        } 
        else
        {
            temp = currLocation.x;
        }
        
        if(position == temp)
        {
            Disable();
        } 
        else if (sr.color.Equals(faded))
        {
            Enable();
        }
    }

    public void Disable()
    {
        sr.color = faded;
    }

    public void Enable()
    {
        sr.color = normal;
    }
}
