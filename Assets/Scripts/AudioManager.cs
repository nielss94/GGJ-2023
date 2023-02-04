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
    
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSourceObject, clip.length);
    }
}
