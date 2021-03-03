using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainSpawning : MonoBehaviour
{
    public GameObject Grain;
    public List<GameObject> numberOfGain = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnMouseDown()
    {
        if (numberOfGain.Count < 3)
        {
            GameObject newGrain = Instantiate(Grain, transform.position, transform.rotation);
            numberOfGain.Add(newGrain);
        }
    }
}
