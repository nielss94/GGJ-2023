using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public Tile rootTile;
    public int assignedToTeam = 0;

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
