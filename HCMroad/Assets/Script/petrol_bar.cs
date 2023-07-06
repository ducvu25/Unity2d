using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petrol_bar : MonoBehaviour
{
   
    public Slider slider;
    // gán giá trị xăng lớn nhất
    public void setMaxPetrol(int petrol)
    {
        slider.maxValue = petrol;
        slider.value = petrol;
    }
    // gán giá trị xăng
   public void  setPetrol(int petrol)
    {
        slider.value = petrol;
    }
}
