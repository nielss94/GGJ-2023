using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnGameOver = delegate {  };
    public static GameManager Instance;
    
    [SerializeField] private float gameTime;
    [SerializeField] private TextMeshProUGUI gameTimeText;

    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private GameObject endScreen;

    private bool gameStarted = false;
    public bool GameStarted => gameStarted;
    
    [SerializeField] private WorldUIPoints worldUIPointsPrefab;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerSpawner.OnAllPlayersSpawned += OnAllPlayersSpawned;
    }

    private void Start()
    {
        SetGameTime();
    }

    private void OnAllPlayersSpawned()
    {
        gameStarted = true;
    }

    private void Update()
    {
        if (!gameStarted) return;
        
        if (gameTime > 0 )
        {
            SetGameTime();
        }
        else
        {
            gameTimeText.text = "TIME'S UP!";
            gameStarted = false;
            OnGameOver?.Invoke();
            endScreen.SetActive(true);
            AudioManager.Instance.PlayEndScreenTheme();
        }
    }

    public void SpawnPoints(Vector3 position, int points, int team)
    {
        var worldUIPoints = Instantiate(worldUIPointsPrefab, position, Quaternion.identity);
        worldUIPoints.SetPoints(points, team);
    }
    
    private void SetGameTime()
    {
        gameTime -= Time.deltaTime;
        // show minutes and seconds with leading zeros
        gameTimeText.text = $"{(int) gameTime / 60:00}:{(int) gameTime % 60:00}";
    }
}
