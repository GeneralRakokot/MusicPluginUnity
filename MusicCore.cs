using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MusicLibrary;

public static class MusicCore
{
    private static GameObject MusicRoot;
    private static Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();
    private static Dictionary<string, ClipControl> clipControls = new Dictionary<string, ClipControl>();
    private static string lastChangedName;

    public static void Play(string name, float fade = 0f)
    {
        if (!IsMusicChanged(name))
        {
            //Debug.Log("No music changes");
            return;
        }
        if (!tracks.ContainsKey(name))
        {
            //Debug.Log("Music not found");
            return;
        }

        AudioClip clip = tracks[name];
        ClipControl clipControl = GetClipControl(name, clip);

        foreach (ClipControl _clipControl in  clipControls.Values )
        {
            _clipControl.Fade(fade);
        }

        clipControl.Unfade(fade);

        //if (isFating)
        //{
        //    EndFating();
        //    main_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
        //    main_MusicSource.Play();
        //}
        //else if (fateTime == 0)
        //{
        //    main_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
        //    main_MusicSource.Play();
        //}
        //else
        //{
        //    auxiliary_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
        //    auxiliary_MusicSource.Play();
        //    StartFating(fateTime);
        //}
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
        foreach (Track track in _tracks)
        {
            tracks[track.MusicName] = track.clip;
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




    //private static AudioSource main_MusicSource;
    //private static AudioSource auxiliary_MusicSource;

    //private static float fateTimeLeft = 0f;
    //private static bool isFating = false;

    //public static void Enter()
    //{
    //    {
    //        main_MusicSource = CreateAudioSource("MainMusic");
    //        auxiliary_MusicSource = CreateAudioSource("AuxMusic");
    //    }
    //}

    //public static void Play(string MusicName, float fateTime = 2f)
    //{
    //    if (isFating) 
    //    {
    //        EndFating();
    //        main_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
    //        main_MusicSource.Play();
    //    }
    //    else if(fateTime == 0)
    //    {
    //        main_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
    //        main_MusicSource.Play();
    //    }
    //    else
    //    {
    //        auxiliary_MusicSource.clip = Resources.Load<AudioClip>(MusicName);
    //        auxiliary_MusicSource.Play();
    //        StartFating(fateTime);
    //    }
    //}

    //private static void StartFating(float time)
    //{
    //    auxiliary_MusicSource.volume = 0f;
    //    fateTimeLeft = time;
    //    isFating = true;
    //    //MonoBehAnchor.OnTimeUpdated += Update;
    //}

    //private static void EndFating()
    //{
    //    main_MusicSource.Stop();
    //    main_MusicSource.volume = 1f;

    //    auxiliary_MusicSource.volume = 1f;

    //    AudioSource buffer = main_MusicSource;
    //    main_MusicSource = auxiliary_MusicSource;
    //    auxiliary_MusicSource = buffer;
    //    isFating = false;
    //    //MonoBehAnchor.OnTimeUpdated -= Update;
    //}

    //private static void Update()
    //{
    //    fateTimeLeft -= Time.deltaTime;

    //    main_MusicSource.volume = Mathf.Lerp(main_MusicSource.volume, 0f, Time.deltaTime / fateTimeLeft);
    //    auxiliary_MusicSource.volume = Mathf.Lerp(auxiliary_MusicSource.volume, 1f, Time.deltaTime / fateTimeLeft);

    //    if (fateTimeLeft < 0)
    //    {
    //        EndFating();
    //    }
    //}

    //private static AudioSource CreateAudioSource(string name)
    //{
    //    GameObject EmptyObject = new GameObject(name);
    //    //EmptyObject.transform.parent = Global.Anchor.transform;
    //    return EmptyObject.AddComponent<AudioSource>();
    //}

    //private static bool isPlaying()
    //{
    //    if (main_MusicSource.isPlaying || auxiliary_MusicSource.isPlaying) { return true; }
    //    return false;
    //}
}
