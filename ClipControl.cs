using UnityEngine;
using System.Collections;

public class ClipControl : MonoBehaviour
{
    private AudioSource audioSource;
    private bool idle;
    private IEnumerator coroutine;

    public void Unfade(float time)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = VolumeCorutine(1, time);
        StartCoroutine(coroutine);
    }

    public void Fade(float time)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = VolumeCorutine(0, time);
        StartCoroutine(coroutine);
    }


    private IEnumerator VolumeCorutine(float endVal, float duration)
    {
        float percent = 0f;

        if (endVal != 0f)
        {
            audioSource.Play();
            idle = false;
        }

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            audioSource.volume = Mathf.Lerp(audioSource.volume, endVal, percent * percent);
            yield return null;
        }

        if (endVal == 0f)
        {
            audioSource.Stop();
            idle = true;
        }
    }

    public void SetMusic(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = clip;
    }

    public bool IsIdle()
    {
        return idle;
    }
}
