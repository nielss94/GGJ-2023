using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressAnyToJoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1TextMesh;
    [SerializeField] private TextMeshProUGUI player2TextMesh;
    
    [Header("Aesthetics")]
    [SerializeField] private float fadeOutTime = 1.0f;
    
    private PlayerSpawner playerSpawner;

    private void Start()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        playerSpawner.OnSpawnedPlayer += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var playerNumber = playerInput.playerIndex + 1;
        var playerTextMesh = playerNumber == 1 ? player1TextMesh : player2TextMesh;
        
        Debug.Log("Fade");

        playerTextMesh.DOFade(0, fadeOutTime);
    }
}
