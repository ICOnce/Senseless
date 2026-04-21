using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource _footstepAudioSource;

    [SerializeField] public AudioClip FootstepClip;

    void Awake()
    {
        _footstepAudioSource = GetComponent<AudioSource>();
    }

    protected void PlayFootstepClip()
    {
        _footstepAudioSource.Stop();
        _footstepAudioSource.clip = FootstepClip;
        _footstepAudioSource.Play();
    }
}
