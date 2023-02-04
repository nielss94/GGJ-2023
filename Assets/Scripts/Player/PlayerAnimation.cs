using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerSporeGun playerSporeGun;
    
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSporeGun = GetComponent<PlayerSporeGun>();
        
        playerSporeGun.OnSprayingChanged += OnSprayingChanged;
    }

    private void OnSprayingChanged(bool spraying)
    {
        animator.SetBool("Spraying", spraying);
    }

    private void Update()
    {
        animator.SetFloat("Speed", playerMovement.MoveDirection.magnitude);
    }
}
