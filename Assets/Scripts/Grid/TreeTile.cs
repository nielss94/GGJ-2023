using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeTile : MonoBehaviour
{
    public List<GameObject> trees;
    [SerializeField] private float[] possibleRotations;
    private void Start()
    {
        Instantiate(trees[Random.Range(0, trees.Count)], transform.position, Quaternion.Euler(0,0,possibleRotations[Random.Range(0, possibleRotations.Length)]), transform);
    }
}
