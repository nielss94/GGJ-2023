using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public delegate void OnPlayerStayAction(GameObject player);
    public event OnPlayerStayAction OnPlayerStay;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerStay?.Invoke(other.gameObject);
        }
    }
}
