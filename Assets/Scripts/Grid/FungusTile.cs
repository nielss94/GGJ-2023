using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusTile : MonoBehaviour
{
    public ParticleSystem team1Particles;
    public ParticleSystem team2Particles;

    private void Start()
    {
        SetTeam(GetComponent<WorldTile>().Team);
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
