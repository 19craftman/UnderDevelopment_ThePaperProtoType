using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHammer : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private RuntimeAnimatorController secondAnim;
    public void drop()
    {
        cam = Camera.main.gameObject;
        StartCoroutine(DropObject());
    }
    IEnumerator DropObject()
    {
        float timeElapsed = 0;
        Vector3 startPos = cam.transform.position;
        Vector3 shakePos = startPos;
        int count = 0;
        float moveTime = .3f;

        while (timeElapsed < moveTime)
        {
            float offsetY = ((moveTime - timeElapsed) / moveTime) * 0.2f;

            if (count % 2 != 0)
            {
                offsetY = -offsetY;
            }
            shakePos.y += offsetY;

            cam.transform.position = shakePos;
            timeElapsed += Time.deltaTime;
            count++;
            yield return null;
        }
        cam.transform.position = startPos;
    }
    
    public void changeAnim()
    {
        gameObject.AddComponent<MoveableObjects>();
        GetComponent<Animator>().runtimeAnimatorController = secondAnim;
        transform.position = new Vector2(4.440001f, -4.43f);
    }
}
