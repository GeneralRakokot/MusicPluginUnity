using UnityEngine;

public class ClipControl : MonoBehaviour
{
    AudioSource audioSource;

    public void Unfade(float time)
    {
        audioSource.Play();
    }

    public void Fade(float time)
    {
        audioSource.Stop();
    }

   public void SetMusic(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = clip;
    }
}
