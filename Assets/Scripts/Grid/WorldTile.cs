using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] private Material unassignedMaterial;
    [SerializeField] private Material team1Material;
    [SerializeField] private Material team2Material;

    [SerializeField] private TileType tileType;

    [SerializeField] private MeshRenderer meshRenderer;
    
    public TileType TileType
    {
        get { return tileType; }
    }
    
    public Tile rootTile;
    private TileTrigger tileTrigger;
    private int team = 0;
    
    public int Team
    {
        get { return team; }
    }
    
    public void SetTeam(int team)
    {
        this.team = team;
        UpdateMaterial();
    }
    
    public void SetRootTile(Tile rootTile)
    {
        this.rootTile = rootTile;
        tileTrigger = rootTile.GetComponentInChildren<TileTrigger>();
        tileTrigger.OnPlayerStay += OnPlayerStayTile;
    }
    
    private void UpdateMaterial()
    {
        switch (team)
        {
            case 0:
                meshRenderer.material = unassignedMaterial;
                break;
            case 1:
                meshRenderer.material = team1Material;
                break;
            case 2:
                meshRenderer.material = team2Material;
                break;
        }
    }

    public Tile[] GetNeighbours()
    {
        Level level = Level.Instance;
        return level.GetNeighbours(rootTile);
    }
    
    [ContextMenu("Log Neighbours")]
    public void LogNeighbours()
    {
        Tile[] neighbours = GetNeighbours();
        foreach (var neighbour in neighbours)
        {
            Debug.Log(neighbour.name);
        }
    }

    private void OnPlayerStayTile(GameObject player)
    {
        if (tileType != TileType.Fungus) return;

        if (player.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement) && 
            player.TryGetComponent<PlayerTeam>(out PlayerTeam playerTeam))
        {
            // Check if the player's pivot is over fungus tile
            var overlaps = Physics.OverlapSphere(player.transform.position, 0f);
            foreach (var overlap in overlaps)
            {
                if (overlap.transform.parent == null) continue;
                var overlapParent = overlap.transform.parent.GetComponentInChildren<TileHolder>();
                if (overlapParent == null) continue;
                
                if (overlapParent.CurrentTile.TileType == TileType.Fungus)
                {
                    if (team == playerTeam.Team)
                    {
                        playerMovement.SetBoost();
                    } else if (team != playerTeam.Team && team != 0)
                    {
                        playerMovement.SetSlow();
                    }
                } else if (overlapParent.CurrentTile.TileType != TileType.Fungus)
                {
                    playerMovement.ResetSpeed();
                }
            }
        }
    }

    private void OnDisable()
    {
        if (tileTrigger == null) return;
        
        tileTrigger.OnPlayerStay -= OnPlayerStayTile;
    }
}
