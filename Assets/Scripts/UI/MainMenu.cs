using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu;

    public void OnGameStart()
    {
        StartCoroutine(LoadLevelRoutine());
    }
    
    public void OnGameExit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private IEnumerator LoadLevelRoutine()
    {
        // TODO: load management scene
        var progress = SceneManager.LoadSceneAsync("BaseScene", LoadSceneMode.Single);

        while (!progress.isDone)
        {
            // TODO: show loading indicator
            yield return null;
        }
    }
}
