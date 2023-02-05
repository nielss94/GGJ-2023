using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public GameObject audioSourcePrefab;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioMixerGroup musicMixer;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip endscreenThemeClip;
    
    
    private void Awake()
    {
        Instance = this;
    }
    
    public GameObject PlaySound(AudioClip clip, Vector3 position, bool loop = false, float volume = 1f, bool sfx = true)
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = sfx ? sfxMixer : musicMixer;
        audioSource.Play();

        if (!loop)
        {
            Destroy(audioSourceObject, clip.length);
        }

        return audioSourceObject;
    }

    public void PlayEndScreenTheme()
    {
        musicSource.clip = endscreenThemeClip;
        musicSource.volume = .75f;
        musicSource.Play();
    }
}
