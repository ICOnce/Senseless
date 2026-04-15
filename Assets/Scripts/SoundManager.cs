using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] public AudioClip BackgroundAmbience;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        //audioSource.playOnAwake = false;
        PlayClip(BackgroundAmbience);
    }

    protected void PlayClip(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
