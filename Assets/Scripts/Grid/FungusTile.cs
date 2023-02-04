using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FungusTile : MonoBehaviour
{
    
    public ParticleSystem team1Particles;
    public ParticleSystem team2Particles;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private Ease ease;
    
    
    public Transform fadeInTransform;
    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
        fadeInTransform.localScale = Vector3.zero;
        fadeInTransform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
    }

    public void SetTeam(int team)
    {
        switch (team)
        {
            case 1:
                team1Particles.Play();
                break;
            case 2:
                team2Particles.Play();
                break;
        }
    }
}
