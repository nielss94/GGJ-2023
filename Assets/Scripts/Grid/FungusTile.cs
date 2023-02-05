using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class FungusTile : MonoBehaviour
{
    
    public ParticleSystem team1Particles;
    public ParticleSystem team2Particles;
    [SerializeField] private float growSpeed = 4f;
    [SerializeField] private float deathSpeed = 1f;
    
    [SerializeField] private Ease ease;
    [SerializeField] private bool isDead = false;
    [SerializeField] private Light pointLight;


    public MeshRenderer renderer;
    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
        
        // Flip values to put emphasis on positive/negative effect
        var modifiedGrowSpeed = growSpeed;
        var modifiedDeathSpeed = deathSpeed;
        if (isDead)
        {
            modifiedGrowSpeed = deathSpeed;
            modifiedDeathSpeed = growSpeed;
        }
        
        renderer.material.DOFloat(1, "_Grow", modifiedGrowSpeed).OnComplete(() =>
        {
            if (isDead)
            {
                renderer.material.SetFloat("_Bulge_Power", 0f);
                renderer.material.DOFloat(1f, "_Death", modifiedDeathSpeed);
                renderer.material.DOFloat(-0.03f, "_Shrivel", modifiedDeathSpeed);
                pointLight.DOIntensity(0f, modifiedDeathSpeed);
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
