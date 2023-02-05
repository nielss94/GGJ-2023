using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TileHolder : MonoBehaviour
{
    [Header("Fungus scoring")] 
    [SerializeField] private float baseTileWidth = 1.73f;
    [SerializeField] private int enokiCheckRange = 1;
    [SerializeField] private int fungusCheckRange = 2;
    [SerializeField] private int deadScore = 0;
    [SerializeField] private int fungusScore = 1;
    [SerializeField] private int enokiScore = 3;
    [SerializeField] private int rootSchroomScore = 15;
    [SerializeField] private int carcassScore = 30;
    [Header("Prefabs")]
    [SerializeField] private WorldTile emptyTilePrefab;
    [SerializeField] private WorldTile rockTilePrefab;
    [SerializeField] private WorldTile mushroomTilePrefab;
    [SerializeField] private WorldTile deadTilePrefab;
    [SerializeField] private WorldTile fungusTilePrefab;
    [SerializeField] private WorldTile enokiPrefab;
    [SerializeField] private WorldTile treeTilePrefab;
    [SerializeField] private WorldTile rootTilePrefab;
    [SerializeField] private WorldTile carcassTilePrefab;
    [SerializeField] private WorldTile rootShroomPrefab;

    private WorldTile currentTile;
    private Tile tile;


    public WorldTile CurrentTile
    {
        get { return currentTile; }
    }

    public void Init(Tile tile)
    {
        this.tile = tile;
    }
    
    public void SetTile(TileType tileType, int team = 0)
    {
        SpawnTile(tileType);
        currentTile.SetTeam(team);
    }
    
    private void SpawnTile(TileType tileType)
    {
        if (currentTile != null)
        {
            Destroy(currentTile.gameObject);
        }
        
        switch (tileType)
        {
            case TileType.Empty:
                currentTile = Instantiate(emptyTilePrefab, transform);
                break;
            case TileType.Rock:
                currentTile = Instantiate(rockTilePrefab, transform);
                break;
            case TileType.Mushroom:
                currentTile = Instantiate(mushroomTilePrefab, transform);
                break;
            case TileType.Fungus:
                PlaceFungus();
                break;
            case TileType.Tree:
                currentTile = Instantiate(treeTilePrefab, transform);
                break;
            case TileType.Root:
                currentTile = Instantiate(rootTilePrefab, transform);
                break;
            case TileType.Carcass:
                currentTile = Instantiate(carcassTilePrefab, transform);
                currentTile.SetFungusScore(carcassScore);
                break;
            case TileType.RootShroom:
                currentTile = Instantiate(rootShroomPrefab, transform);
                currentTile.SetFungusScore(rootSchroomScore);
                break;
        }
        
        currentTile.SetRootTile(tile);
    }

    private void PlaceFungus()
    {
        // Check range to carcass/root
        var overlaps = Physics.OverlapSphere(transform.position, baseTileWidth * fungusCheckRange);
        var closestRange = float.MaxValue;
        var isDead = true;
        
        foreach (var overlap in overlaps)
        {
            if (overlap.transform.parent == null) continue;

            var tile = overlap.transform.parent.GetComponentInChildren<TileHolder>();
            if (tile == null) continue;
            
            // Check if no root or carcass nearby
            if (tile.CurrentTile.TileType != TileType.Carcass && tile.CurrentTile.TileType != TileType.Root) continue;
            
            isDead = false;

            var range = Vector3.Distance(transform.position, overlap.transform.position);
            closestRange = Mathf.Min(closestRange, range);
        }

        if (isDead)
        {
            // Dead fungus
            currentTile = Instantiate(deadTilePrefab, transform);
            currentTile.SetFungusScore(deadScore);
            return;
        }
        
        // Check if within enoki range
        if (closestRange <= baseTileWidth * enokiCheckRange)
        {
            // Enoki
            currentTile = Instantiate(enokiPrefab, transform);
            currentTile.SetFungusScore(enokiScore);
        }
        else
        {
            // Fungus
            currentTile = Instantiate(fungusTilePrefab, transform);
            currentTile.SetFungusScore(fungusScore);
        }
    }
}
