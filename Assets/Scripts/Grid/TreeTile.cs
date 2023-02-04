using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeTile : MonoBehaviour
{
    public List<GameObject> trees;
    
    private void Start()
    {
        Instantiate(trees[Random.Range(0, trees.Count)], transform);
    }
}
