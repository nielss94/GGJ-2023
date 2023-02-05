using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class RootShroomTile : MonoBehaviour
{
    public GameObject team1RootShroom;
    public GameObject team2RootShroom;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private Ease moveEase;
    [SerializeField] private float shakeStrength = 0.1f;

    private GameObject currentRootShroom;
    private void Start()
    {
        var worldTile = GetComponent<WorldTile>();
        SetTeam(worldTile.Team);
        currentRootShroom.transform.localScale = Vector3.one * 0.5f;
        currentRootShroom.transform.localPosition = Vector3.down * 0.5f;
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                currentRootShroom = Instantiate(team1RootShroom, transform);
                break;
            case 2:
                currentRootShroom = Instantiate(team2RootShroom, transform);
                break;
        }
        
        currentRootShroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(scaleEase);
        currentRootShroom.transform.DOLocalMoveY(0.25f, fadeInSpeed).SetEase(moveEase);
        // currentRootShroom.transform.DOShakePosition(fadeInSpeed, shakeStrength);
    }
}
