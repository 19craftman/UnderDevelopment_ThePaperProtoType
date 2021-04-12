using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChicken : MonoBehaviour
{
    [SerializeField] private Sprite sit, stand;
    private GameObject child;
    private SpriteRenderer sr;
    private void Awake()
    {
        child = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(standing());
    }

    IEnumerator standing()
    {
        child.SetActive(true);
        sr.sprite = stand;
        yield return new WaitForSeconds(.2f);
        child.SetActive(false);
        sr.sprite = sit;
    }
}
