using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TileHolder : MonoBehaviour
{
    [SerializeField] private WorldTile emptyTilePrefab;
    [SerializeField] private WorldTile rockTilePrefab;
    [SerializeField] private WorldTile mushroomTilePrefab;
    [SerializeField] private WorldTile fungusTilePrefab;
    [SerializeField] private WorldTile treeTilePrefab;
    [SerializeField] private WorldTile rootTilePrefab;
    [SerializeField] private WorldTile carcassTilePrefab;

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
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Rock:
                currentTile = Instantiate(rockTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Mushroom:
                currentTile = Instantiate(mushroomTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Fungus:
                currentTile = Instantiate(fungusTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Tree:
                currentTile = Instantiate(treeTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Root:
                currentTile = Instantiate(rootTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
            case TileType.Carcass:
                currentTile = Instantiate(carcassTilePrefab, transform);
                // currentTile.rootTile = tile;
                currentTile.SetRootTile(tile);
                break;
        }
    }
}
