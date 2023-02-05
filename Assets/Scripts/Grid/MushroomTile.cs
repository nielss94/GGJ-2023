using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MushroomTile : MonoBehaviour
{
    public Mushroom team1mushroom;
    public Mushroom team2mushroom;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private Ease moveEase;
    
    private Mushroom currentMushroom;
    private Vector3 initialPosition;
    private void Start()
    {
        var worldTile = GetComponent<WorldTile>();
        SetTeam(worldTile.Team);
        currentMushroom.StartSpawning();
        initialPosition = worldTile.rootTile.transform.position;
        currentMushroom.transform.localScale = Vector3.one * 0.5f;
        currentMushroom.transform.localPosition = Vector3.down * 0.5f;
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                currentMushroom = Instantiate(team1mushroom, transform);
                break;
            case 2:
                currentMushroom = Instantiate(team2mushroom, transform);
                break;
        }
        
        currentMushroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(scaleEase);
        // currentMushroom.transform.DOMove(currentMushroom.transform.TransformVector(Vector3.zero), fadeInSpeed).SetEase(moveEase);
        currentMushroom.transform.DOLocalMoveY(0.35f, fadeInSpeed).SetEase(moveEase);
    }
}
