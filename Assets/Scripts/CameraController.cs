using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    private GameObject cam;
    private CameraMove cameraMove;
    enum Direction {left, right, up, down };
    [SerializeField] private Direction dir;
    private float hoverTime;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        cameraMove = cam.GetComponent<CameraMove>();
        hoverTime = 0f;
    }


    private void LateUpdate()
    {
        if (GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            OnOverlap();
        }
    }

    private void OnMouseDown()
    {
        hoverTime = .1f;
        OnOverlap();
    }

    private void OnOverlap()
    {
        hoverTime += Time.deltaTime;

        if(hoverTime >=.01f)
        {
            hoverTime = 0f;
            if (dir == Direction.left)
            {
                cameraMove.Left();
            }
            else if (dir == Direction.right)
            {
                cameraMove.Right();
            }
            else if (dir == Direction.up)
            {
                cameraMove.Up();
            }
            else if (dir == Direction.down)
            {
                cameraMove.Down();
            }
        }
    }

    private void OnMouseExit()
    {
        hoverTime = 0f;
    }
}
