using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    
    private Tile[][] map;
    [SerializeField] private Tile tilePrefab;

    public void Generate()
    {
        var level = new GameObject("New Level").AddComponent<Level>();
        map = null;
        map = new Tile[width][];
        
        for (int x = 0; x < width; x++)
        {
            map[x] = new Tile[height];
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * xOffset, 0, y * yOffset);
                if (x % 2 == 1)
                {
                    pos.z -= yOffset / 2;
                }
                // var newTile = Instantiate(tilePrefab, pos, Quaternion.identity, level.transform);
                var newTile = PrefabUtility.InstantiatePrefab(tilePrefab) as Tile;
                newTile.transform.parent = level.transform;
                newTile.transform.position = pos;
                
                newTile.name = $"Tile {x} {y}";
                map[x][y] = newTile;
                newTile.SetDesignTileColor();
            }
        }

        level.width = width;
        level.height = height;
    }
}
