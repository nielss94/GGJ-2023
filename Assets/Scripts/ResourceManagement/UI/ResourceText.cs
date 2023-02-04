using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResourceText : MonoBehaviour
{
    [SerializeField] private ResourceHolder resourceHolder;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        resourceHolder.OnSporesChanged += UpdateText;
    }

    private void UpdateText(int amount)
    {
        textMesh.text = amount + "";
    }
}
