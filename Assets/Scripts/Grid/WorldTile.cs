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

    public TileType TileType
    {
        get { return tileType; }
    }
    
    public Tile rootTile;
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
    
    private void UpdateMaterial()
    {
        // TODO: If more meshrenderers this wont work
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        
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
}
