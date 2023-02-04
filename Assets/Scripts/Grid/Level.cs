using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    public Tile[][] map;
    public int width;
    public int height;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // create map from children
        map = new Tile[width][];
        for (int x = 0; x < width; x++)
        {
            map[x] = new Tile[height];
            for (int y = 0; y < height; y++)
            {
                map[x][y] = transform.GetChild(x * height + y).GetComponent<Tile>();
            }
        }
        
    }

    public Tile[] GetNeighbours(Tile tile)
    {
        var tileIndex = GetTileIndex(tile);

        if (tileIndex != Vector2Int.zero)
        {
            var topLeft = GetTile(tileIndex + new Vector2Int(1, 0));
            var topRight = GetTile(tileIndex + new Vector2Int(1, -1));
            var left = GetTile(tileIndex + new Vector2Int(0, 1));
            var right = GetTile(tileIndex + new Vector2Int(0, -1));
            var bottomLeft = GetTile(tileIndex + new Vector2Int(-1, 0));
            var bottomRight = GetTile(tileIndex + new Vector2Int(-1, -1));
            
            var neighbours = new Tile[]
            {
                topLeft,
                topRight,
                left,
                right,
                bottomLeft,
                bottomRight,
            };
            
            return neighbours.Where(n => n != null).ToArray();
        }

        return new Tile[]{};
    }
    
    private Tile GetTile(Vector2Int index)
    {
        if (index.x < 0 || index.x >= width || index.y < 0 || index.y >= height)
        {
            return null;
        }
        
        return map[index.x][index.y];
    }
    
    private Vector2Int GetTileIndex(Tile tile)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log(map[x].Length);
                if (map[x][y] == tile)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        
        return Vector2Int.zero;
    }
}
