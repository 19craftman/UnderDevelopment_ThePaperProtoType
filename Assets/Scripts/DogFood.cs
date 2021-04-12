using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFood : MonoBehaviour
{
    [SerializeField] private GameObject food;
    private void OnDestroy()
    {
       GameObject a = Instantiate(food, new Vector3(.048f, -12.31f, 0f), Quaternion.identity);
        a.SetActive(true);
    }
}
