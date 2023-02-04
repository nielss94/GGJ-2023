using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPointPlayer1;
    [SerializeField] private Transform spawnPointPlayer2;

    private PlayerInputManager playerInputManager;
    
    public event Action<PlayerInput> OnSpawnedPlayer;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        playerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var playerNumber = playerInput.playerIndex + 1;
        var playerGameObject = playerInput.gameObject;
        
        SetPlayerPosition(playerGameObject, playerNumber);
        SetPlayerVirtaulCamera(playerGameObject, playerNumber);
        ExcludeCamera(playerGameObject, playerNumber);
        
        OnSpawnedPlayer?.Invoke(playerInput);
    }
    
    private void SetPlayerPosition(GameObject playerGameObject, int number)
    {
        playerGameObject.transform.position = number == 1 ? spawnPointPlayer1.position : spawnPointPlayer2.position;
    }

    private void SetPlayerVirtaulCamera(GameObject playerGameObject, int number)
    {
        var layer =  LayerMask.NameToLayer("Player " + number);
        var playerVirtualCamera = playerGameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        playerVirtualCamera.gameObject.layer = layer;
    }
    
    private void ExcludeCamera(GameObject playerGameObject, int number)
    {
        var playerCamera = playerGameObject.GetComponentInChildren<Camera>();
        var otherPlayerLayer = LayerMask.NameToLayer("Player " + (number == 1 ? 2 : 1));
        playerCamera.cullingMask &= ~(1 << otherPlayerLayer);
    }
}
