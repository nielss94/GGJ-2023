using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private int team;
    
    public int Team { get { return team; } }
    
    public void SetTeam(int team)
    {
        this.team = team;
    }
}
