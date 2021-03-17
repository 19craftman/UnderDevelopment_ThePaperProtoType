using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraMove cameraMove;
    enum Direction {left, right, up, down };
    [SerializeField] private Direction dir;
    private float hoverTime;
    AudioManager am;
    Sound s;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        am.playDialog("TitleScreen1");
        s = am.soundLookUp("TitleScreen1");
        hoverTime = 0f;
    }


    private void LateUpdate()
    {
        if (s.played && GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            OnOverlap();
        }
    }

    private void OnMouseDown()
    {
        if (s.played)
        {
            hoverTime = .1f;
            OnOverlap();
        }
    }

    private void OnOverlap()
    {
        hoverTime += Time.deltaTime;

        if(hoverTime >=.01f)
        {
            hoverTime = 0f;
            if (dir == Direction.left)
            {
                cameraMove.left();
            }
            else if (dir == Direction.right)
            {
                cameraMove.right();
            }
            else if (dir == Direction.up)
            {
                cameraMove.up();
            }
            else if (dir == Direction.down)
            {
                cameraMove.down();
            }
        }
    }

    private void OnMouseExit()
    {
        hoverTime = 0f;
    }
}
