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
   [SerializeField] private float boostModifier = 1.5f;
   [SerializeField] private float slowModifier = 0.5f;
   
   private PlayerInput playerInput;
   private Rigidbody rb;
   private CinemachineVirtualCamera virtualCamera;
   private InputAction moveAction;
   private PlayerSpawner playerSpawner;
   
   private Vector2 moveDirection = Vector2.zero;
   
   public bool AllowedToMove = false;
   
   public Vector2 MoveDirection
   {
      get { return moveDirection; }
   }
   private bool allowedToMove = false;
   private float modifiedSpeed;

   private void Awake()
   {
      playerSpawner = FindObjectOfType<PlayerSpawner>();
      playerInput = GetComponent<PlayerInput>();
      rb = GetComponent<Rigidbody>();
      virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
      moveAction = playerInput.actions["move"];
      modifiedSpeed = moveSpeed;
      
      playerInput = GetComponent<PlayerInput>();
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
      
      moveDirection = !AllowedToMove ? Vector2.zero : moveAction.ReadValue<Vector2>();
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
      rb.MovePosition(transform.position + (cameraForward * moveDirection.y + cameraRight * moveDirection.x) * modifiedSpeed * Time.deltaTime);
      
      // Rotate player
      if(moveDirection.magnitude > 0.1f)
      {
         var direction = cameraForward * moveDirection.y + cameraRight * moveDirection.x;
         model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.LookRotation(direction), 0.15f);
      }
      
   }

   public void SetBoost()
   {
      modifiedSpeed = moveSpeed * boostModifier;
   }
   
   public void SetSlow()
   {
      modifiedSpeed = moveSpeed * slowModifier;
   }

   public void ResetSpeed()
   {
      modifiedSpeed = moveSpeed;
   }
}
