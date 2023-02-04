using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerTeam))]
public class PlayerSporeGun : MonoBehaviour
{
    public event Action OnPickUpSpore = delegate {  };
    public event Action OnPlaceSpore = delegate {  };
    [SerializeField] private ResourceHolder resourceHolder;

    private PlayerTeam playerTeam;

    private void Awake()
    {
        playerTeam = GetComponent<PlayerTeam>();
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

    private void OnPlaceFungus()
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
                OnPlaceSpore?.Invoke();
            }
        }
    }
}
