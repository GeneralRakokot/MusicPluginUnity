//using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static MusicLibrary;

public static class MusicCore
{
    private static GameObject MusicRoot;
    private static Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();
    private static Dictionary<string, ClipControl> clipControls = new Dictionary<string, ClipControl>();
    private static string lastChangedName;
    private static float lastChangedFadeTime;

    public static void Play(string name, float fadeTime = 1f)
    {
        Debug.Log("Play");
        if (!IsMusicChanged(name))
        {
            //Debug.Log("No music changes");
            return;
        }
        if (!tracks.ContainsKey(name))
        {
            lastChangedName = name;
            lastChangedFadeTime = fadeTime;
            //Debug.Log("Music not found");
            return;
        }

        FadeAll(fadeTime);

        AudioClip clip = tracks[name];
        ClipControl clipControl = GetClipControl(name, clip);

        clipControl.Unfade(fadeTime);
    }

    public static void Stop(float fadeTime = 1f)
    {
        FadeAll(fadeTime);
        lastChangedName = null;
    }

    private static bool IsMusicChanged(string name)
    {
        if (lastChangedName == name)
        {
            return false;
        }
        lastChangedName = name;
        return true;
    }

    public static void SetTracksLibrary(Track[] _tracks)
    {
        ClearIdle();
        foreach (Track track in _tracks)
        {
            tracks[track.MusicName] = track.clip;
            if (lastChangedName == track.MusicName)
            {
                lastChangedName = null;
                Play(track.MusicName, lastChangedFadeTime);
            }
        }

    }

    private static ClipControl GetClipControl(string name, AudioClip clip)
    {
        if (clipControls.ContainsKey(name))
        {
            return clipControls[name];
        }

        if (MusicRoot == null)
        {
            MusicRoot = new GameObject("MusicRoot");
            Object.DontDestroyOnLoad(MusicRoot);
        }

        GameObject obj = new GameObject(name);
        obj.transform.SetParent(MusicRoot.transform);
        ClipControl clipControl  = obj.AddComponent<ClipControl>();
        clipControl.SetMusic(clip);
        clipControls[name] = clipControl;

        return clipControl;
    }

    private static void ClearIdle()
    {
        foreach (ClipControl clipControl in clipControls.Values)
        {
            if (clipControl.IsIdle()) GameObject.Destroy(clipControl.gameObject);
        }
    }

    private static void FadeAll(float time)
    {
        if (MusicRoot == null || MusicRoot.transform.childCount == 0) return;
        foreach (int i in Enumerable.Range(0, MusicRoot.transform.childCount))
        {
            Transform obj = MusicRoot.transform.GetChild(i);
            ClipControl clipControl = obj.gameObject.GetComponent<ClipControl>();
            if (clipControl != null)
            {
                clipControl.Fade(time);
            }
        }
    }
}
