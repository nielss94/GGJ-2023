using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UseSporeProgress : MonoBehaviour
{
    
    public PlayerSporeGun PlayerSporeGun;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSporeGun == null)
        {
            return;
        }
        
        slider.value = PlayerSporeGun.PlaceTimer / PlayerSporeGun.MaxPlaceTimer;
    }
}
