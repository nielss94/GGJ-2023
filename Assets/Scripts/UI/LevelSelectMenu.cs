using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
    [SerializeField] private GameObject levelSelectButtonPrefab;
    [SerializeField] private GameObject levelButtonContainer;
    
    private LevelManager levelManager;
    
    private void Awake() {
        levelManager = LevelManager.Instance;
        PopulateLevelButtons(levelManager.Levels);
    }

    private void PopulateLevelButtons(string[] levels) {
        for (var i = 0; i < levels.Length; i++) {
            var newButton = Instantiate(levelSelectButtonPrefab, levelButtonContainer.transform);
            var levelName = levels[i];
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnLevelSelected(levelName));
            
            if (i == 0) {
                EventSystem.current.SetSelectedGameObject(newButton);                                   
            }
        }
    }

    private void OnLevelSelected(string level) {
        levelManager.LoadNextLevel(level);
        levelManager.UnloadLevel("MainMenu");
    }
}
