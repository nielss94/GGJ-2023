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
        var tree = trees[Random.Range(0, trees.Count)];
        var instantiated = Instantiate(tree, transform);
        instantiated.transform.localEulerAngles = new Vector3(-90, 0, possibleRotations[Random.Range(0, possibleRotations.Length)]);
    }
}
