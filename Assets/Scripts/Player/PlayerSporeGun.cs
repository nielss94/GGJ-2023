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
    public event Action<bool> OnSprayingChanged = delegate {  };
    public event Action OnPickUpSpore = delegate {  };
    public event Action OnPlaceSpore = delegate {  };
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
                OnPickUpSpore?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (startPlaceTimer && resourceHolder.CanSpendSpores(1))
        {
            OnSprayingChanged?.Invoke(true);
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
        OnSprayingChanged?.Invoke(false);
    }

    private void OnPlaceFungus(InputAction.CallbackContext ctx)
    {
        startPlaceTimer = true;
    }

    private void OnCancelFungusPlacement(InputAction.CallbackContext ctx)
    {
        startPlaceTimer = false;
        placeTimer = initialPlaceTimer;
    }
    
    private void PlaceFungus()
    {
        // Check if player is on a tile
        var overlaps = Physics.OverlapSphere(transform.position, 0f);
        foreach (var overlap in overlaps)
        {
            if (overlap.transform.parent == null) continue;
            
            var tile = overlap.transform.parent.GetComponentInChildren<TileHolder>();
            if (tile != null)
            {
                Debug.Log(tile.CurrentTile.TileType);
                
                if (tile.CurrentTile.TileType != TileType.Empty) return;
                
                if (resourceHolder.TrySpendSpores(1))
                {
                    tile.SetTile(TileType.Fungus, playerTeam.Team);
                    OnPlaceSpore?.Invoke();
                    OnSprayingChanged?.Invoke(false);
                }
            }
        }
        
        // Raycast to hit WorldTile undearneath player
        // var ray = new Ray(transform.position, Vector3.down);
        // if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("TileCollider")))
        // {
        //     // If the raycast hits a WorldTile, then get the tile and place a fungus on it
        //     var tile = hit.transform.parent.GetComponentInChildren<TileHolder>();
        //     
        //     // Check if the tile is already occupied
        //     
        //     Debug.Log(tile.CurrentTile.TileType);
        //     
        //     if (tile.CurrentTile.TileType != TileType.Empty)
        //     {
        //         return;
        //     }
        //
        //     if (resourceHolder.TrySpendSpores(1))
        //     {
        //         tile.SetTile(TileType.Fungus, playerTeam.Team);
        //         OnPlaceSpore?.Invoke();
        //         OnSprayingChanged?.Invoke(false);
        //     }
        // }
    }
}
