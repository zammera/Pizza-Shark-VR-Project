using UnityEngine;

public class PlaySoundFunc : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundEffect;

    public void PlaySound()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}