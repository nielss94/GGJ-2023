using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TileType
{
    Empty,
    Rock,
    Mushroom,
    Fungus,
    Tree,
    Root,
    Carcass,
    RootShroom,
    Enoki,
    Dead,
    Water,
    FoodShroom,
    Bridge
}

public class Tile : MonoBehaviour
{
    [Header("Design")]
    [SerializeField] private Transform designTile;
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material rockMaterial;
    [SerializeField] private Material mushroomMaterial;
    [SerializeField] private Material fungusMaterial;
    [SerializeField] private Material treeMaterial;
    [SerializeField] private Material rootMaterial;
    [SerializeField] private Material carcassMaterial;
    [SerializeField] private Material rootShroomMaterial;
    [SerializeField] private Material enokiMaterial;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material foodShroomMaterial;
    [SerializeField] private Material bridgeMaterial;
    
    [Header("Tile")]
    [SerializeField] private TileHolder tileHolder;
    [SerializeField] private TileType tileType;
    [SerializeField] private int team;

    public TileType Type { get; set; }

    private void Start()
    {
        designTile.GetComponent<MeshRenderer>().enabled = false;
        
        tileHolder.Init(this);
        tileHolder.SetTile(tileType, team, true);
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
            case TileType.Fungus:
                designTile.GetComponent<MeshRenderer>().material = fungusMaterial;
                break;
            case TileType.Tree:
                designTile.GetComponent<MeshRenderer>().material = treeMaterial;
                break;
            case TileType.Root:
                designTile.GetComponent<MeshRenderer>().material = rootMaterial;
                break;
            case TileType.Carcass:
                designTile.GetComponent<MeshRenderer>().material = carcassMaterial;
                break;
            case TileType.RootShroom:
                designTile.GetComponent<MeshRenderer>().material = rootShroomMaterial;
                break;
            case TileType.Enoki:
                designTile.GetComponent<MeshRenderer>().material = enokiMaterial;
                break;
            case TileType.Water:
                designTile.GetComponent<MeshRenderer>().material = waterMaterial;
                break;
            case TileType.FoodShroom:
                designTile.GetComponent<MeshRenderer>().material = foodShroomMaterial;
                break;
            case TileType.Bridge:
                designTile.GetComponent<MeshRenderer>().material = bridgeMaterial;
                break;
        }
    }
}
