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

    private void OnMouseOver()
    {
        hoverTime += Time.deltaTime;

        if(hoverTime >=.5f)
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
