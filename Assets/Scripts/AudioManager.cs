using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public GameObject audioSourcePrefab;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public GameObject PlaySound(AudioClip clip, Vector3 position, bool loop = false, float volume = 1f)
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();

        if (!loop)
        {
            Destroy(audioSourceObject, clip.length);
        }

        return audioSourceObject;
    }
    
}
