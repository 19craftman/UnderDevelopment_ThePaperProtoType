using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyNote : MonoBehaviour
{
    [SerializeField] private GameObject sticky;

    private void OnMouseDown()
    {
        sticky.SetActive(!sticky.activeInHierarchy);
    }
}
