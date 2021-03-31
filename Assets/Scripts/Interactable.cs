using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    // Start is called before the first frame update
    void Start()
    {
        highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if(gameObject.name == "rope")
        {
            Debug.Log("work");
        }
        highlight.SetActive(true);
    }

    private void OnMouseDrag()
    {
        highlight.SetActive(false);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnDestroy()
    {
        Destroy(highlight);
    }
}
