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
    [SerializeField] private bool isDead = false;
    [SerializeField] private Light pointLight;


    public MeshRenderer renderer;
    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
        renderer.material.DOFloat(1, "_Grow", fadeInSpeed).OnComplete(() =>
        {
            if (isDead)
            {
                renderer.material.SetFloat("_Bulge_Power", 0f);
                renderer.material.DOFloat(1f, "_Death", fadeInSpeed);
                renderer.material.DOFloat(-0.03f, "_Shrivel", fadeInSpeed);
                pointLight.DOIntensity(0f, fadeInSpeed);
            }
        });
        // fadeInTransform.localScale = Vector3.zero;
        // fadeInTransform.DOScale(Vector3.one, fadeInSpeed).SetEase(ease);
    }

    public void SetTeam(int team)
    {
        if (isDead) return;
        
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
