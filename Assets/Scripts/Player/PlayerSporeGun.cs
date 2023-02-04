using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerTeam), typeof(PlayerMovement))]
public class PlayerSporeGun : MonoBehaviour
{
    [SerializeField] private ResourceHolder resourceHolder;
    [SerializeField] private float initialPlaceTimer = 1.0f;

    public float MaxPlaceTimer
    {
        get { return initialPlaceTimer; }
    }

    private PlayerInput playerInput;
    private PlayerTeam playerTeam;
    private PlayerMovement playerMovement;
    
    private bool startPlaceTimer = false;
    private float placeTimer = 0;

    public float PlaceTimer
    {
        get { return placeTimer; }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        var placeFungus = playerInput.actions["Place Fungus"];
        
        placeFungus.canceled += OnCancelFungusPlacement;
        placeFungus.performed += OnPlaceFungus;

        
        playerTeam = GetComponent<PlayerTeam>();
    }

    private void Start()
    {
        placeTimer = initialPlaceTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Spore spore))
        {
            if (spore.Team != playerTeam.Team && spore.Team != 0)
            {
                return;
            }

            var cost = 1;

            if (resourceHolder.CanAddSpores(cost))
            {
                spore.sourceMushroom.TakeSpore(spore.gameObject, true);
                resourceHolder.AddSpores(cost);
            }
        }
    }

    private void Update()
    {
        if (startPlaceTimer && resourceHolder.CanSpendSpores(1))
        {
            placeTimer -= Time.deltaTime;
            playerMovement.AllowedToMove = false;
        }
        else
        {
            ResetPlacing();
        }

        if (placeTimer <= 0)
        {
            PlaceFungus();
            ResetPlacing();
        }
    }

    private void ResetPlacing()
    {
        playerMovement.AllowedToMove = true;
        placeTimer = initialPlaceTimer;
    }

    private void OnPlaceFungus(InputAction.CallbackContext ctx)
    {
        Debug.Log("Start pressing the button!");
        startPlaceTimer = true;
    }

    private void OnCancelFungusPlacement(InputAction.CallbackContext ctx)
    {
        Debug.Log("Stop pressing the button!");
        startPlaceTimer = false;
        placeTimer = initialPlaceTimer;
    }
    
    private void PlaceFungus()
    {
        // Raycast to hit WorldTile undearneath player
        var ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("TileCollider")))
        {
            // If the raycast hits a WorldTile, then get the tile and place a fungus on it
            var tile = hit.transform.parent.GetComponentInChildren<TileHolder>();
            
            // Check if the tile is already occupied
            
            Debug.Log(tile.CurrentTile.TileType);
            
            if (tile.CurrentTile.TileType != TileType.Empty)
            {
                return;
            }

            if (resourceHolder.TrySpendSpores(1))
            {
                tile.SetTile(TileType.Fungus, playerTeam.Team);
            }
        }
    }
}
