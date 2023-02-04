using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 2.0f;
   
   private PlayerInput playerInput;
   private InputAction moveAction;

   private void Awake()
   {
      playerInput = GetComponent<PlayerInput>();
      moveAction = playerInput.actions["move"];
   }

   void Start()
   {
      moveAction.Enable();
   }

   void Update()
   {
      var moveDirection = moveAction.ReadValue<Vector2>();
      transform.position = transform.position + new Vector3(moveDirection.x, 0,moveDirection.y) * moveSpeed * Time.deltaTime;
   }
}
