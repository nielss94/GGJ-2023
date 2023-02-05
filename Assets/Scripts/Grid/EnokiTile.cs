using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnokiTile : MonoBehaviour
{
    public GameObject team1Enoki;
    public GameObject team2Enoki;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private Ease moveEase;
    
    private GameObject currentEnoki;
    private void Start()
    {
        var worldTile = GetComponent<WorldTile>();
        SetTeam(worldTile.Team);
        currentEnoki.transform.localScale = Vector3.one * 0.5f;
        currentEnoki.transform.localPosition = Vector3.down * 0.5f;
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                currentEnoki = Instantiate(team1Enoki, transform);
                break;
            case 2:
                currentEnoki = Instantiate(team2Enoki, transform);
                break;
        }
        
        currentEnoki.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(scaleEase);
        currentEnoki.transform.DOLocalMoveY(0f, fadeInSpeed).SetEase(moveEase);
    }
}
