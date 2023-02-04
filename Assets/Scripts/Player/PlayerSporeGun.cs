using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSporeGun : MonoBehaviour
{
    [SerializeField] private ResourceHolder resourceHolder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Spore spore))
        {
            spore.sourceMushroom.TakeSpore(spore.gameObject, true);
            resourceHolder.AddSpores(1);
        }
    }
}
