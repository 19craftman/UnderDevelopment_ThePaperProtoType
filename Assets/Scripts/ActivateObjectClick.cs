using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectClick : MonoBehaviour
{
    [SerializeField] private GameObject toActivate;

    private void OnMouseDown()
    {
        toActivate.SetActive(!toActivate.activeInHierarchy);
    }
}
