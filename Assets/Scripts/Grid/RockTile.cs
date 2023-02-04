using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTile : MonoBehaviour
{
    public List<GameObject> rocks;
    
    private void Start()
    {
        Instantiate(rocks[Random.Range(0, rocks.Count)], transform);
    }
}
