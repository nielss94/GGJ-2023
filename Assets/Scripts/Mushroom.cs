using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mushroom : MonoBehaviour
{ 
    [SerializeField] private GameObject sporePrefab;
    [SerializeField] private float spawnCooldown = 3f;
    [SerializeField] private float spawnRadius = 1f;

    private bool isSpawning = true;
    private bool onCooldown = false;
    private SporeContainer sporeContainer;
    private List<GameObject> spores = new List<GameObject>();

    private void Awake()
    {
        sporeContainer = GetComponentInChildren<SporeContainer>();
    }

    private void Update()
    {
        if (isSpawning && !onCooldown)
        {
            StartCoroutine(SpawnSpore());
        }
    }

    public GameObject TakeSpore(bool destroySpore = false)
    {
        if (spores.Count > 0)
        {
            var spore = spores[0];
            spores.RemoveAt(0);
            if (destroySpore)
            {
                Destroy(spore);
            }
            SetSporePositions();
            return spore;
        }
        return null;
    }
    
    private IEnumerator SpawnSpore()
    {
        onCooldown = true;
        var newSpore = Instantiate(sporePrefab, (sporeContainer.transform.position), Quaternion.identity, sporeContainer.transform);
        spores.Add(newSpore);
        SetSporePositions();
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }

    private void SetSporePositions()
    {
        for (int i = 0; i < spores.Count; i++)
        {
            var angle = i * Mathf.PI * 2 / spores.Count;
            var x = Mathf.Cos(angle) * spawnRadius;
            var z = Mathf.Sin(angle) * spawnRadius;
            // spores[i].transform.localPosition = new Vector3(x, 0, z);
            // Start routine to slerp to the new position
            StartCoroutine(SlerpSpore(spores[i], new Vector3(x, 0, z)));
            
        }
    }
    
    private IEnumerator SlerpSpore(GameObject spore, Vector3 targetPosition)
    {
        var startPosition = spore.transform.localPosition;
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            spore.transform.localPosition = Vector3.Slerp(startPosition, targetPosition, t);
            yield return null;
        }
    }
}
