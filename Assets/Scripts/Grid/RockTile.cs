using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTile : MonoBehaviour
{
    public List<GameObject> rocks;
    [SerializeField] private float[] possibleRotations;
    private void Start()
    {
        Instantiate(rocks[Random.Range(0, rocks.Count)], transform.position, Quaternion.Euler(0,0,possibleRotations[Random.Range(0, possibleRotations.Length)]), transform);
    }
}
