using System;
using System.Collections;
using System.Collections.Generic;
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
            Time.timeScale = 0;
            gameStarted = false;
            OnGameOver?.Invoke();
            endScreen.SetActive(true);
        }
    }

    private void SetGameTime()
    {
        gameTime -= Time.deltaTime;
        // show minutes and seconds with leading zeros
        gameTimeText.text = $"{(int) gameTime / 60:00}:{(int) gameTime % 60:00}";
    }
}
