using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu;
    [SerializeField] private GameObject playButton;
    
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.actions["Navigate"].performed += OnNavigate;
    }

    public void OnNavigate(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        if (value != Vector2.zero && EventSystem.current.currentSelectedGameObject == null && mainMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }

    public void OnGameStart()
    {
        StartCoroutine(LoadLevelRoutine());
    }
    
    public void OnGameExit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
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
