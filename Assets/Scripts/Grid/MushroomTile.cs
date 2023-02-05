using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MushroomTile : MonoBehaviour
{
    public Mushroom team1mushroom;
    public Mushroom team2mushroom;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease ease;
    
    private Mushroom currentMushroom;
    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
        currentMushroom.StartSpawning();
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                currentMushroom = Instantiate(team1mushroom, transform);
                currentMushroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
                break;
            case 2:
                currentMushroom = Instantiate(team2mushroom, transform);
                currentMushroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
                break;
        }
    }
}
