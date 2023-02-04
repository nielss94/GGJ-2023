using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int oddOffset;
    //
    // private Tiles[,] map;
    //
    // private void SetupMap()
    // {
    //     map = null;
    //     map = new Tiles[mapWidth, mapHeight];
    //     
    //     for (int y = 0; y < mapHeight; y++)
    //     {
    //         for (int x = 0; x < mapWidth; x++)
    //         {
    //             map[x, y] = Tiles.Wall;
    //         }
    //     }
    // }
}
