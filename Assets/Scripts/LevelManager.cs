using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private String[] levels;
    
    private int currentLevelIndex = 0;
    private string currentLoadedLevelName;
    private GameManager gameManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        LoadNextLevel();
        
        gameManager = GameManager.Instance;
        if (gameManager != null) gameManager.OnGameOver += OnGameOver;
    }

    public void LoadNextLevel()
    {
        if (levels.Length == 0) return;

        if (currentLevelIndex < levels.Length)
        {
            if (currentLoadedLevelName == null || currentLoadedLevelName.Length > 0)
            {
                StartCoroutine(UnloadLevelRoutine(currentLoadedLevelName));
            }
            StartCoroutine(LoadLevelRoutine(levels[currentLevelIndex]));
            currentLevelIndex++;
        }
        else
        {
            currentLevelIndex = 0;
            LoadNextLevel();
        }
    }

    public IEnumerator LoadLevelRoutine(string nextLevelName)
    {
        var progress = SceneManager.LoadSceneAsync(nextLevelName, LoadSceneMode.Additive);

        while (!progress.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevelName));
        currentLoadedLevelName = nextLevelName;
        if (gameManager == null) gameManager = GameManager.Instance;
    }
    
    public IEnumerator UnloadLevelRoutine(string levelName)
    {
        var progress = SceneManager.UnloadSceneAsync(levelName);

        while (!progress.isDone)
        {
            yield return null;
        }
    }
    
    private void OnGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    
    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(5f);
        
        LoadNextLevel();
    }
    
    private void OnDestroy()
    {
        gameManager.OnGameOver -= OnGameOver;
    }
    
}
