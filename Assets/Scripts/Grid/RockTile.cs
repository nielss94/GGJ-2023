using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTile : MonoBehaviour
{
    public List<GameObject> rocks;
    [SerializeField] private float[] possibleRotations;
    private void Start()
    {
        var rock = rocks[Random.Range(0, rocks.Count)];
        var instantiated = Instantiate(rock, transform);
        instantiated.transform.localEulerAngles = new Vector3(-90, 0, possibleRotations[Random.Range(0, possibleRotations.Length)]);
    }
}
