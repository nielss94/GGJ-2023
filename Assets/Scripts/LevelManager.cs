using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct LevelRef
    {
        public string Name;
        public string SceneName;
    }
    
    public static LevelManager Instance;
    public LevelRef[] Levels => levels;

    [SerializeField] private LevelRef[] levels;
    
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
        
        // LoadNextLevel();
        
        // gameManager = GameManager.Instance;
        // if (gameManager != null) gameManager.OnGameOver += OnGameOver;
    }

    public void LoadNextLevel(string level = "")
    {
        if (levels.Length == 0) return;

        if (currentLoadedLevelName != null && currentLoadedLevelName.Length > 0)
        {
            StartCoroutine(UnloadLevelRoutine(currentLoadedLevelName));
        }
        
        if (level.Length > 0)
        {
            for (var i = 0; i < levels.Length; i++)
            {
                if (levels[i].SceneName == level)
                {
                    currentLevelIndex = i;
                    break;
                }
            }
        }
        
        if (currentLevelIndex < levels.Length)
        {
            StartCoroutine(LoadLevelRoutine(levels[currentLevelIndex].SceneName));
            currentLevelIndex++;
        }
        else
        {
            currentLevelIndex = 0;
            LoadNextLevel();
        }
    }

    public void UnloadLevel(string levelName) {
        StartCoroutine(UnloadLevelRoutine(levelName));
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
    
    private IEnumerator UnloadLevelRoutine(string levelName)
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
