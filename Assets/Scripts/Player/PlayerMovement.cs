using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 2.0f;
   
   private PlayerInput playerInput;
   private Rigidbody rigidbody;
   private CinemachineVirtualCamera virtualCamera;
   private InputAction moveAction;
   
   private Vector2 moveDirection = Vector2.zero;

   private void Awake()
   {
      playerInput = GetComponent<PlayerInput>();
      rigidbody = GetComponent<Rigidbody>();
      virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
      moveAction = playerInput.actions["move"];
   }

   void Start()
   {
      moveAction.Enable();
   }

   void Update()
   {
      moveDirection = moveAction.ReadValue<Vector2>();
   }

   private void FixedUpdate()
   {
      var cameraForward = virtualCamera.transform.forward;
      var cameraRight = virtualCamera.transform.right;
      cameraForward.y = 0;
      cameraRight.y = 0;
      cameraForward.Normalize();
      cameraRight.Normalize();
      rigidbody.MovePosition(transform.position + (cameraForward * moveDirection.y + cameraRight * moveDirection.x) * moveSpeed * Time.deltaTime);
   }
}
