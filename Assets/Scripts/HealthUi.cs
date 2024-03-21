using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
        [SerializeField]
      RectTransform Helthui;


    private void OnEnable()
    {
        GetComponent<Healthmanager>().currentHealth.OnValueChanged += changeHealth;
    }

    private void OnDisable()
    {
        GetComponent<Healthmanager>().currentHealth.OnValueChanged -= changeHealth;

    }

    private void changeHealth(int previousValue, int newValue)
    {
        Helthui.transform.localScale = new Vector3(newValue/100f,1,1);
       
    }
}
