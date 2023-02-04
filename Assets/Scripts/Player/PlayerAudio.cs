using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private PlayerSporeGun sporeGun;

    [SerializeField] private float sporePickUpSoundInterval = 2f;
    private int sporePickUpCount = 0;
    private float latestSporePickUpTime = 0f;
    
    //list of pickup sounds
    [SerializeField] private List<AudioClip> sporePickUpSounds = new List<AudioClip>();
    
    
    private void Awake()
    {
        sporeGun = GetComponent<PlayerSporeGun>();
        
        sporeGun.OnPickUpSpore += PlayPickUpSporeSound;
    }

    private void Update()
    {
        if (Time.time - latestSporePickUpTime > sporePickUpSoundInterval)
        {
            sporePickUpCount = 0;
        }
    }

    private void PlayPickUpSporeSound()
    {
        AudioManager.Instance.PlaySound(sporePickUpSounds[sporePickUpCount], transform.position);
        sporePickUpCount = Mathf.Clamp(sporePickUpCount + 1, 0, 3);
        latestSporePickUpTime = Time.time;
    }
}
