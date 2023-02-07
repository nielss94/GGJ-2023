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
    [SerializeField] private float initialPlaceTimer = 0f;
    [SerializeField] private float maxPlaceTimer = 1;
    public float MaxPlaceTimer => maxPlaceTimer;


    private PlayerInput playerInput;
    private PlayerTeam playerTeam;
    private PlayerMovement playerMovement;
    
    private bool startPlaceTimer = false;
    private float placeTimer = 0;
    public float PlaceTimer => placeTimer;

    private bool gameOver = false;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        var placeFungus = playerInput.actions["Place Fungus"];
        
        placeFungus.canceled += OnCancelFungusPlacement;
        placeFungus.performed += OnPlaceFungus;

        
        playerTeam = GetComponent<PlayerTeam>();
        
        GameManager.Instance.OnGameOver += OnGameOver;
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
        if (gameOver) return;
        
        if (startPlaceTimer && resourceHolder.CanSpendSpores(1))
        {
            placeTimer += Time.deltaTime;
            playerMovement.AllowedToMove = false;
        }
        else
        {
            ResetPlacing();
        }

        if (placeTimer >= maxPlaceTimer)
        {
            PlaceFungus();
            ResetPlacing();
        }
    }

    private void OnGameOver()
    {
        gameOver = true;
    }
    
    private void ResetPlacing()
    {
        playerMovement.AllowedToMove = true;
        placeTimer = initialPlaceTimer;
        OnSprayingChanged?.Invoke(false);
    }

    private void OnPlaceFungus(InputAction.CallbackContext ctx)
    {
        if (!CanPlaceFungus()) return;
        startPlaceTimer = true;
        if (resourceHolder.CanSpendSpores(1))
        {
            OnSprayingChanged?.Invoke(true);
        }
    }

    private void OnCancelFungusPlacement(InputAction.CallbackContext ctx)
    {
        startPlaceTimer = false;
        placeTimer = initialPlaceTimer;
        OnSprayingChanged?.Invoke(false);
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
                if (tile.CurrentTile.TileType == TileType.Carcass)
                {
                    if (resourceHolder.TrySpendSpores(1))
                    {
                        tile.SetTile(TileType.FoodShroom, playerTeam.Team);
                        OnPlaceSpore?.Invoke();
                        OnSprayingChanged?.Invoke(false);
                    }
                    
                    return;
                }

                if (tile.CurrentTile.TileType == TileType.Root)
                {
                    if (resourceHolder.TrySpendSpores(1))
                    {
                        tile.SetTile(TileType.RootShroom, playerTeam.Team);
                        OnPlaceSpore?.Invoke();
                        OnSprayingChanged?.Invoke(false);
                    }

                    return;
                }

                if (tile.CurrentTile.TileType == TileType.Empty)
                {
                    if (resourceHolder.TrySpendSpores(1))
                    {
                        tile.SetTile(TileType.Fungus, playerTeam.Team);
                        OnPlaceSpore?.Invoke();
                        OnSprayingChanged?.Invoke(false);
                    }

                    return;
                }
            }
        }
    }

    private bool CanPlaceFungus()
    {
        var overlaps = Physics.OverlapSphere(transform.position, 0f);
        foreach (var overlap in overlaps)
        {
            if (overlap.transform.parent == null) continue;
            
            var tile = overlap.transform.parent.GetComponentInChildren<TileHolder>();
            if (tile != null)
            {
                if (tile.CurrentTile.TileType == TileType.Empty ||
                    tile.CurrentTile.TileType == TileType.Carcass ||
                    tile.CurrentTile.TileType == TileType.Root)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
