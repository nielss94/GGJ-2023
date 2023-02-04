using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   
   [SerializeField] private GameObject model;
   [SerializeField] private float moveSpeed = 2.0f;
   
   private PlayerInput playerInput;
   private Rigidbody rigidbody;
   private CinemachineVirtualCamera virtualCamera;
   private InputAction moveAction;
   private PlayerSpawner playerSpawner;
   
   private Vector2 moveDirection = Vector2.zero;
   private bool allowedToMove = false;
   
   public Vector2 MoveDirection
   {
      get { return moveDirection; }
   }
   private void Awake()
   {
      playerSpawner = FindObjectOfType<PlayerSpawner>();
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
      if (!playerSpawner.AllPlayersSpawned)
      {
         return;
      }
      
      moveDirection = moveAction.ReadValue<Vector2>();
   }

   private void FixedUpdate()
   {
      if (!playerSpawner.AllPlayersSpawned)
      {
         return;
      }
      
      var cameraForward = virtualCamera.transform.forward;
      var cameraRight = virtualCamera.transform.right;
      cameraForward.y = 0;
      cameraRight.y = 0;
      cameraForward.Normalize();
      cameraRight.Normalize();
      
      // Move player
      rigidbody.MovePosition(transform.position + (cameraForward * moveDirection.y + cameraRight * moveDirection.x) * moveSpeed * Time.deltaTime);
      
      // Rotate player
      if(moveDirection.magnitude > 0.1f)
      {
         var direction = cameraForward * moveDirection.y + cameraRight * moveDirection.x;
         model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.LookRotation(direction), 0.15f);
      }
      
   }
}
