using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : MonoBehaviour
{
    public List<GameObject> debris;
    [SerializeField] private float[] possibleRotations;
    private void Start()
    {
        if (debris.Count <= 0) return;
        
        var debrisObject = debris[Random.Range(0, debris.Count)];
        var instantiated = Instantiate(debrisObject, transform);
        instantiated.transform.localEulerAngles = new Vector3(-90, 0, possibleRotations[Random.Range(0, possibleRotations.Length)]);
    }
}
