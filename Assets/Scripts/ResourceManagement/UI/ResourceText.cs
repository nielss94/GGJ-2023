using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResourceText : MonoBehaviour
{
    public ResourceHolder resourceHolder;
    private TextMeshProUGUI textMesh;

    private bool subscribed = false;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (resourceHolder == null || subscribed)
        {
            return; 
        }
        
        SubscribeToSporeChange();
    }

    private void SubscribeToSporeChange()
    {
        resourceHolder.OnSporesChanged += UpdateText;
        subscribed = true;
    }

    private void UpdateText(int amount)
    {
        textMesh.text = amount + "";
    }
}
