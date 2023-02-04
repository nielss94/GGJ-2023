using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSporeProgress : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    
    public PlayerSporeGun PlayerSporeGun;

    [SerializeField] private Image image;

    // Update is called once per frame
    void Update()
    {
        if (PlayerSporeGun == null)
        {
            return;
        }
        
        image.fillAmount = PlayerSporeGun.PlaceTimer / PlayerSporeGun.MaxPlaceTimer;
        
        // image always face camera
        image.transform.parent.LookAt(cameraTransform);
    }
}
