using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty,
    Rock
}

public class Tile : MonoBehaviour
{
    public Transform tileHolder;
    public TileType tileType;
    public GameObject emptyTilePrefab;
    public GameObject rockTilePrefab;

    private GameObject currentTile;

    private void OnValidate()
    {
        if (currentTile != null)
        {
            DestroyImmediate(currentTile);
        }
        switch (tileType)
        {
            case TileType.Empty:
                currentTile = Instantiate(emptyTilePrefab, tileHolder);
                break;
            case TileType.Rock:
                currentTile = Instantiate(rockTilePrefab, tileHolder);
                break;
        }
    }
}
