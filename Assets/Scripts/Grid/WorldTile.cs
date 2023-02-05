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
    private int fungusScore = 0;
    public int FungusScore => fungusScore;
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
    
    public void SetFungusScore(int fungusScore, int team)
    {
        this.fungusScore = fungusScore;

        if (GameManager.Instance.GameStarted)
        {
            GameManager.Instance.SpawnPoints(transform.position, fungusScore, team);
        }
    }
    
    private void UpdateMaterial()
    {
        if (meshRenderer == null)
        {
            return;
        }
        
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
        if (tileType != TileType.Fungus && tileType != TileType.Enoki && tileType != TileType.RootShroom) return;

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
                
                if (overlapParent.CurrentTile.TileType == TileType.Fungus || 
                    overlapParent.CurrentTile.TileType == TileType.Enoki || 
                    overlapParent.CurrentTile.TileType == TileType.RootShroom)
                {
                    if (team == playerTeam.Team)
                    {
                        playerMovement.SetBoost();
                    } else if (team != playerTeam.Team && team != 0)
                    {
                        playerMovement.SetSlow();
                    }
                }
                
                if (overlapParent.CurrentTile.TileType != TileType.Fungus && 
                    overlapParent.CurrentTile.TileType != TileType.Enoki && 
                    overlapParent.CurrentTile.TileType != TileType.RootShroom)
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
