using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceAmountUI : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private ResourceText resourceTextPlayer1;
    [SerializeField] private ResourceText resourceTextPlayer2;

    // Start is called before the first frame update
    void Awake()
    {
        playerSpawner.OnSpawnedPlayer += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var number = playerInput.playerIndex + 1;

        if (number == 1)
        {
            resourceTextPlayer1.resourceHolder = playerInput.GetComponent<ResourceHolder>();
        }
        else
        {
            resourceTextPlayer2.resourceHolder = playerInput.GetComponent<ResourceHolder>();
        }
    }
}
