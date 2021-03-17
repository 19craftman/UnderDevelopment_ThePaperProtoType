using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyDog : MonoBehaviour
{
    Rect box;
    private bool isAsleep;
    float marginX = .05f;
    int verticleRays = 4;
    public GameObject winText;
    AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Piano.correct && !isAsleep)
        {
            box = new Rect(
                GetComponent<BoxCollider2D>().bounds.min.x,
                GetComponent<BoxCollider2D>().bounds.min.y,
                GetComponent<BoxCollider2D>().bounds.size.x,
                GetComponent<BoxCollider2D>().bounds.size.y
            );
            isAsleep = true;
        }
        else if(isAsleep && !winText.activeInHierarchy)
        {
            winText.SetActive(true);
            am.playDialog("EngGameTemp");
        }
    }

    bool abovePiano()
    {
            Vector2 startPoint = new Vector2(box.xMin + marginX, box.center.y);
            Vector2 endPoint = new Vector2(box.xMax - marginX, box.center.y);

            RaycastHit2D hitInfo;

            float distance = 20;

            bool connected = false;

            for (int i = 0; i < verticleRays; i++)
            {
                float lerpAmount = (float)i / (float)(verticleRays - 1);
                Vector3 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);
                //Debug.Log(distance);

                hitInfo = Physics2D.Raycast(origin, Vector2.down, distance, 256);

                if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Piano"))
                {
                    connected = true;
                    break;
                }
            }

            return connected;
    }
}
