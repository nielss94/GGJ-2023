using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float oddOffset;
    [SerializeField] public int mapScale = 1;
    
    private Tile[,] map;
    [SerializeField] private Tile tilePrefab;

    public void Generate()
    {
        var levelTransform = new GameObject("New Level").transform;
        map = null;
        map = new Tile[width, height];
        
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                Vector3 pos = new Vector3(x * mapScale, 0, y * mapScale);
                if (y % 2 == 1)
                {
                    pos.x += oddOffset;
                }
                var newTile = Instantiate(tilePrefab, pos, Quaternion.identity, levelTransform);
                map[x, y] = newTile;
                newTile.SetDesignTileColor();
            }
        }
    }
}
