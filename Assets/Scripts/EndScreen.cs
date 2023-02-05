using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private TextMeshProUGUI team1ScoreText;
    [SerializeField] private TextMeshProUGUI team2ScoreText;
    
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
        // show scores
        team1ScoreText.text = "Loading...";
        team2ScoreText.text = "Loading...";

        ShowScores();
    }
    
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowScores()
    {
        var worldTiles = FindObjectsOfType<WorldTile>();
        var team1Tiles = worldTiles.Where(tile => tile.Team == 1);
        var team2Tiles = worldTiles.Where(tile => tile.Team == 2);
        
        var team1Score = team1Tiles.Sum(tile => tile.FungusScore);
        var team2Score = team2Tiles.Sum(tile => tile.FungusScore);
        
        team1ScoreText.text = team1Score.ToString();
        team2ScoreText.text = team2Score.ToString();
    }
}
