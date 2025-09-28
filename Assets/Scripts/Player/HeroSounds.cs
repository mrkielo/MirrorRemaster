using UnityEngine;

public class HeroSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [Header("Clips")]
    public AudioClip Walk;
    public AudioClip Jump;
    public AudioClip Die;
    public AudioClip Dash;

    public void PlayClipIfEmpty(AudioClip clip)
    {
        if (audioSource.clip == null || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopClip(AudioClip clip)
    {
        if (audioSource.clip == clip)
        {
            audioSource.Stop();
        }
    }
}
