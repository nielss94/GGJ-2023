using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty,
    Rock,
    Mushroom
}

public class Tile : MonoBehaviour
{
    [Header("Design")]
    [SerializeField] private Transform designTile;
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material rockMaterial;
    [SerializeField] private Material mushroomMaterial;
    
    [Header("Tile")]
    [SerializeField] private Transform tileHolder;
    [SerializeField] private TileType tileType;
    [SerializeField] private WorldTile emptyTilePrefab;
    [SerializeField] private WorldTile rockTilePrefab;
    [SerializeField] private WorldTile mushroomTilePrefab;

    private WorldTile currentTile;

    private void Start()
    {
        designTile.GetComponent<MeshRenderer>().enabled = false;
        SpawnTiles();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            SetDesignTileColor();
        }
    }
    
    public void SetDesignTileColor()
    {
        switch (tileType)
        {
            case TileType.Empty:
                designTile.GetComponent<MeshRenderer>().material = emptyMaterial;
                break;
            case TileType.Rock:
                designTile.GetComponent<MeshRenderer>().material = rockMaterial;
                break;
            case TileType.Mushroom:
                designTile.GetComponent<MeshRenderer>().material = mushroomMaterial;
                break;
        }
    }
    
    private void SpawnTiles()
    {
        switch (tileType)
        {
            case TileType.Empty:
                currentTile = Instantiate(emptyTilePrefab, tileHolder);
                currentTile.rootTile = this;
                break;
            case TileType.Rock:
                currentTile = Instantiate(rockTilePrefab, tileHolder);
                currentTile.rootTile = this;
                break;
            case TileType.Mushroom:
                currentTile = Instantiate(mushroomTilePrefab, tileHolder);
                currentTile.rootTile = this;
                break;
        }
    }
}
