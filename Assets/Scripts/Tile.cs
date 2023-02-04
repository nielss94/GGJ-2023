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
    [Header("Design")]
    [SerializeField] private Transform designTile;
    [SerializeField] private Color emptyColor;
    [SerializeField] private Color rockColor;
    
    [Header("Tile")]
    [SerializeField] private Transform tileHolder;
    [SerializeField] private TileType tileType;
    [SerializeField] private GameObject emptyTilePrefab;
    [SerializeField] private GameObject rockTilePrefab;

    private GameObject currentTile;

    private void Start()
    {
        designTile.GetComponent<MeshRenderer>().enabled = false;
        SpawnTiles();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    designTile.GetComponent<MeshRenderer>().material.color = emptyColor;
                    break;
                case TileType.Rock:
                    designTile.GetComponent<MeshRenderer>().material.color = rockColor;
                    break;
            }
        }
    }
    
    private void SpawnTiles()
    {
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
