using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider s;
    [SerializeField] private Image i;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Slider>();
    }

   public void changeImage()
    {
        i.fillAmount =( s.value+20) / (s.maxValue+20);
    }
}
