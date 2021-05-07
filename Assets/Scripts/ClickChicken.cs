using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChicken : MonoBehaviour
{
    private GameObject child;
    private Animator anim;
    private void Awake()
    {
        child = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        stand();
        anim.SetTrigger("stand");
    }

    private void stand()
    {
        child.SetActive(true);
    }
    public void sit()
    {
        child.SetActive(false);
    }
}
