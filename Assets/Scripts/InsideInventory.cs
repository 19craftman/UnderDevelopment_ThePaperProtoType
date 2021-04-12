﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideInventory : MonoBehaviour
{
    private List<GameObject> containing;
    [SerializeField] private Transform[] inventorySlots;
    [SerializeField] private GameObject inventory;
    // Start is called before the first frame update
    private void Awake()
    {
        containing = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addObject(GameObject item)
    {
        if(containing.Count<4)
        {
            containing.Add(item);
            positionItems();
            return true;
        }
        return false;
    }

    public void removeObject(GameObject item)
    {
        containing.Remove(item);
        item.transform.parent = null;
    }

    private void positionItems()
    {
        int i = 0;
        foreach(GameObject item in containing)
        {
            item.transform.SetParent(inventorySlots[i]);
            item.transform.localPosition = Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        positionItems();
        inventory.SetActive(!inventory.activeInHierarchy);
    }


}