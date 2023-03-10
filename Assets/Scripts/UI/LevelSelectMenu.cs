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

    private void PopulateLevelButtons(LevelManager.LevelRef[] levels) {
		GameObject firstSelect = null;
		
        for (var i = 0; i < levels.Length; i++) {
            var newButton = Instantiate(levelSelectButtonPrefab, levelButtonContainer.transform);
            var levelName = levels[i];
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = levelName.Name;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnLevelSelected(levelName.SceneName));
            
            if (i == 0) {
				firstSelect = newButton;
            }
        }
		
		if (firstSelect != null) {
			EventSystem.current.SetSelectedGameObject(firstSelect.GetComponentInChildren<Button>().gameObject);
		}
    }

    private void OnLevelSelected(string level) {
        levelManager.LoadNextLevel(level);
        levelManager.UnloadLevel("Sce_MainMenu");
    }
}
