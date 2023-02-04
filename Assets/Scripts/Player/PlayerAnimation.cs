using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", playerMovement.MoveDirection.magnitude);
        
    }
}
