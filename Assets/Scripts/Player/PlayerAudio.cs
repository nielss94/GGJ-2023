using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    private PlayerSporeGun sporeGun;
    private PlayerMovement playerMovement;

    [SerializeField] private float sporePickUpSoundInterval = 2f;
    private int sporePickUpCount = 0;
    private float latestSporePickUpTime = 0f;
    
    //list of pickup sounds
    [SerializeField] private List<AudioClip> sporePickUpSounds = new List<AudioClip>();

    [SerializeField] private List<AudioClip> sporePlaceSounds = new List<AudioClip>();
    
    [SerializeField] private List<AudioClip> footstepSounds = new List<AudioClip>();
    
    [SerializeField] private AudioClip[] spraySounds = new AudioClip[3];

    // latest place clip
    private AudioClip latestPlaceClip;
    private AudioClip latestMoveClip;
    
    
    private GameObject activeLoopingSpraySound;

    
    private void Awake()
    {
        sporeGun = GetComponent<PlayerSporeGun>();
        playerMovement = GetComponent<PlayerMovement>();
        
        sporeGun.OnPickUpSpore += PlayPickUpSporeSound;
        sporeGun.OnPlaceSpore += PlayPlaceSporeSound;
        sporeGun.OnSprayingChanged += OnSprayingChanged;
        sporeGun.OnPlaceSpore += OnPlaceSpore;
    }

    private void Update()
    {
        if (Time.time - latestSporePickUpTime > sporePickUpSoundInterval)
        {
            sporePickUpCount = 0;
        }
        
        // if player movedirection is not null, play sound every 0.4 seconds
        if (playerMovement.MoveDirection != Vector2.zero)
        {
            if (Time.time - latestSporePickUpTime > 0.4f)
            {
                PlayMoveSound();
                
                latestSporePickUpTime = Time.time;
            }
        }
        
    }
    
    private void OnPlaceSpore()
    {
        AudioManager.Instance.PlaySound(spraySounds[2], transform.position);
    }


    private void OnSprayingChanged(bool change)
    {
        if (change && activeLoopingSpraySound == null)
        {
            AudioManager.Instance.PlaySound(spraySounds[0], transform.position);
            StartCoroutine(PlayLoopingSoundAfterSeconds(spraySounds[0].length));
        }
        else if (activeLoopingSpraySound != null)
        {
            Destroy(activeLoopingSpraySound);
        }
        
    }

    private IEnumerator PlayLoopingSoundAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        activeLoopingSpraySound = AudioManager.Instance.PlaySound(spraySounds[1], transform.position, true);
    }

    private void PlayPickUpSporeSound()
    {
        AudioManager.Instance.PlaySound(sporePickUpSounds[sporePickUpCount], transform.position);
        sporePickUpCount++;
        sporePickUpCount = Mathf.Clamp(sporePickUpCount, 0, 2);
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
            AudioManager.Instance.PlaySound(latestPlaceClip, transform.position, false, .6f);
        }
    }
    
    private void PlayMoveSound()
    {
        if (latestMoveClip != null)
        {
            // play random sound that was not used before this
            List<AudioClip> unusedSounds = footstepSounds.Where(sound => !sound.Equals(latestMoveClip)).ToList();
            latestMoveClip = unusedSounds[Random.Range(0, unusedSounds.Count)];
            AudioManager.Instance.PlaySound(latestMoveClip, transform.position);
        }
        else
        {
            // play random sound
            latestMoveClip = footstepSounds[Random.Range(0, footstepSounds.Count)];
            AudioManager.Instance.PlaySound(latestMoveClip, transform.position);
        }
    }
    
}
