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
    [SerializeField] private int maxSporeSpawn = 4;

    private PlayerSpawner playerSpawner;
    private bool spawningStarted = false;
    
    private int team = 0;
    private bool isSpawning = true;
    private bool onCooldown = false;
    private SporeContainer sporeContainer;
    private List<GameObject> spores = new List<GameObject>();

    private void Awake()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        sporeContainer = GetComponentInChildren<SporeContainer>();
    }

    private void Start()
    {
        playerSpawner.OnAllPlayersSpawned += () => spawningStarted = true;
        team = GetComponentInParent<WorldTile>().Team;
    }

    private void Update()
    {
        if (isSpawning && !onCooldown && spawningStarted)
        {
            StartCoroutine(SpawnSpore());
        }
    }

    public void TakeSpore(GameObject spore, bool destroySpore = false)
    {
        if (spores.Count > 0)
        {
            var match = spores.Find(g => g == spore);
            spores.RemoveAt(spores.IndexOf(match));
            if (destroySpore)
            {
                Destroy(spore);
            }
            SetSporePositions();
        }
    }
    
    private IEnumerator SpawnSpore()
    {
        if(spores.Count >= maxSporeSpawn)
        {
            yield return new WaitForSeconds(spawnCooldown);
            yield break;
        }
        
        onCooldown = true;
        var newSpore = Instantiate(sporePrefab, (sporeContainer.transform.position), Quaternion.identity, sporeContainer.transform);
        spores.Add(newSpore);
        SetSporePositions();

        var spore = newSpore.GetComponent<Spore>();
        
        spore.sourceMushroom = this;
        spore.Team = team;
        
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
            if (spore == null)
            {
                yield break;
            }
            t += Time.deltaTime * 2f;
            spore.transform.localPosition = Vector3.Slerp(startPosition, targetPosition, t);
            // TODO: scale up after spawning
            yield return null;
        }
    }
}
