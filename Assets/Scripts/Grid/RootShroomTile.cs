using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RootShroomTile : MonoBehaviour
{
    public GameObject team1RootShroom;
    public GameObject team2RootShroom;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease ease;
    
    private GameObject currentRootShroom;
    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                currentRootShroom = Instantiate(team1RootShroom, transform);
                currentRootShroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
                break;
            case 2:
                currentRootShroom = Instantiate(team2RootShroom, transform);
                currentRootShroom.transform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
                break;
        }
    }
}
