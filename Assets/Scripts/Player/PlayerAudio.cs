using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    private PlayerSporeGun sporeGun;

    [SerializeField] private float sporePickUpSoundInterval = 2f;
    private int sporePickUpCount = 0;
    private float latestSporePickUpTime = 0f;
    
    //list of pickup sounds
    [SerializeField] private List<AudioClip> sporePickUpSounds = new List<AudioClip>();

    [SerializeField] private List<AudioClip> sporePlaceSounds = new List<AudioClip>();

    // latest place clip
    private AudioClip latestPlaceClip;
    
    private void Awake()
    {
        sporeGun = GetComponent<PlayerSporeGun>();
        
        sporeGun.OnPickUpSpore += PlayPickUpSporeSound;
        sporeGun.OnPlaceSpore += PlayPlaceSporeSound;
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

    private void PlayPlaceSporeSound()
    {
        if (latestPlaceClip != null)
        {
            // play random sound that was not used before this
            List<AudioClip> unusedSounds = sporePlaceSounds.Where(sound => !sound.Equals(latestPlaceClip)).ToList();
            latestPlaceClip = unusedSounds[Random.Range(0, unusedSounds.Count)];
            AudioManager.Instance.PlaySound(latestPlaceClip, transform.position);
        }
        else
        {
            // play random sound
            latestPlaceClip = sporePlaceSounds[Random.Range(0, sporePlaceSounds.Count)];
            AudioManager.Instance.PlaySound(latestPlaceClip, transform.position);
        }
    }
}
