using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    
    private Tile[,] map;
    [SerializeField] private Tile tilePrefab;

    public void Generate()
    {
        var levelTransform = new GameObject("New Level").transform;
        map = null;
        map = new Tile[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * xOffset, 0, y * yOffset);
                if (x % 2 == 1)
                {
                    pos.z -= yOffset / 2;
                }
                var newTile = Instantiate(tilePrefab, pos, Quaternion.identity, levelTransform);
                newTile.name = $"Tile {x} {y}";
                map[x, y] = newTile;
                newTile.SetDesignTileColor();
            }
        }
    }
}
