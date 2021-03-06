﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideInventory : MonoBehaviour
{
    [SerializeField] private GameObject sun, bHammer;
    private List<GameObject> containing;
    [SerializeField] private Transform[] inventorySlots;
    [SerializeField] private GameObject inventory, game;
    private AudioManager am;
    // Start is called before the first frame update
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
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
            if (item.Equals(sun))
            {
                am.playDialog("SunToolbox");
            } else if(item.Equals(bHammer))
            {
                am.playDialog("bhToolbox");
            } else if (item.CompareTag("Egg"))
            {
                am.playDialog("inventoryEgg");
            }
            containing.Add(item);
            positionItems();
            return true;
        } else
        {
            am.playDialog("Toolbox");
        }
        return false;
    }

    public void removeObject(GameObject item)
    {
        containing.Remove(item);
        item.transform.parent = game.transform;
    }

    private void positionItems()
    {
        int i = 0;
        foreach(GameObject item in containing)
        {
            item.transform.SetParent(inventorySlots[i]);
            item.transform.localPosition = Vector3.zero;
            i++;
        }
    }

    private void OnMouseDown()
    {
        positionItems();
        inventory.SetActive(!inventory.activeInHierarchy);
    }


}
