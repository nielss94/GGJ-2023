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

    [SerializeField] private Material player1Material;
    [SerializeField] private Material player2Material;

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
        
        SetPlayerMaterial(playerGameObject, playerNumber);
        SetPlayerPosition(playerGameObject, playerNumber);
        SetPlayerVirtaulCamera(playerGameObject, playerNumber);
        ExcludeCamera(playerGameObject, playerNumber);
        
        OnSpawnedPlayer?.Invoke(playerInput);
    }
    
    private void SetPlayerMaterial(GameObject playerGameObject, int number)
    {
        // Gadverdamme! Maar ja is Game Jam toch.
        playerGameObject.transform.Find("CH_Gasman").transform.Find("Model").GetComponent<Renderer>().material = number == 1 ? player1Material : player2Material;
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
