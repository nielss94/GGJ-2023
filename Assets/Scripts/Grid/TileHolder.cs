using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TileHolder : MonoBehaviour
{
    [Header("Fungus scoring")] 
    [SerializeField] private float baseTileWidth = 1.73f;
    [SerializeField] private int enokiCheckRange = 1;
    [SerializeField] private int fungusCheckRange = 2;
    [SerializeField] private float checkMargin = 0.5f;
    [SerializeField] private int deadScore = 0;
    [SerializeField] private int fungusScore = 1;
    [SerializeField] private int enokiScore = 3;
    [SerializeField] private int rootSchroomScore = 15;
    [SerializeField] private int mushroomScore = 15;
    [SerializeField] private int carcassScore = 30;
    [SerializeField] private TileType[] boostingTypes;
    
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

    [SerializeField] private AudioClip mushroomSpawnClip;
    [SerializeField] private List<AudioClip> enokiSpawnClips;
    [SerializeField] private AudioClip rootShroomSpawnClip;
    [SerializeField] private List<AudioClip> sporePlaceSounds = new List<AudioClip>();
    
    private AudioClip latestPlaceClip;

    public WorldTile CurrentTile
    {
        get { return currentTile; }
    }

    public void Init(Tile tile)
    {
        this.tile = tile;
    }
    
    public void SetTile(TileType tileType, int team = 0, bool gameStart = false)
    {
        SpawnTile(tileType, gameStart);
        currentTile.SetTeam(team);
    }
    
    private void SpawnTile(TileType tileType, bool gameStart = false)
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
                currentTile.SetFungusScore(mushroomScore);
                if (!gameStart)
                {
                    AudioManager.Instance.PlaySound(mushroomSpawnClip, transform.position);
                }
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
                AudioManager.Instance.PlaySound(rootShroomSpawnClip, transform.position);
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
            if (tile.CurrentTile.TileType != TileType.Carcass && 
                tile.CurrentTile.TileType != TileType.Root &&
                tile.CurrentTile.TileType != TileType.RootShroom) continue;
            
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

        Debug.Log(closestRange);
        // Check if within enoki range
        if (closestRange <= baseTileWidth * enokiCheckRange + checkMargin)
        {
            // Enoki
            currentTile = Instantiate(enokiPrefab, transform);
            currentTile.SetFungusScore(enokiScore);
            
            // play random enoki spawn clip
            var enokiSpawnClip = enokiSpawnClips[Random.Range(0, enokiSpawnClips.Count)];
            AudioManager.Instance.PlaySound(enokiSpawnClip, transform.position);
        }
        else
        {
            // Fungus
            currentTile = Instantiate(fungusTilePrefab, transform);
            currentTile.SetFungusScore(fungusScore);
            PlayPlaceSporeSound();
        }
    }
    
    
    private void PlayPlaceSporeSound()
    {
        if (latestPlaceClip != null)
        {
            // play random sound that was not used before this
            List<AudioClip> unusedSounds = sporePlaceSounds.Where(sound => !sound.Equals(latestPlaceClip)).ToList();
            latestPlaceClip = unusedSounds[Random.Range(0, unusedSounds.Count)];
            AudioManager.Instance.PlaySound(latestPlaceClip, transform.position);
        }
        else
        {
            // play random sound
            latestPlaceClip = sporePlaceSounds[Random.Range(0, sporePlaceSounds.Count)];
            AudioManager.Instance.PlaySound(latestPlaceClip, transform.position, false, .6f);
        }
    }

}


