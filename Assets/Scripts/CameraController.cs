using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraMove cameraMove;
    enum Direction {left, right, up, down };
    [SerializeField] private Direction dir;
    private float hoverTime;
    // Start is called before the first frame update
    void Start()
    {
        hoverTime = 0f;
    }
    Ray ray;
    RaycastHit hit;
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
