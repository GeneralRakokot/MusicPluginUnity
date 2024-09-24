using UnityEngine;



public class MusicLibrary : MonoBehaviour
{
    [System.Serializable]
    public struct Track
    {
        public string MusicName;
        public AudioClip clip;
    }

    public Track[] tracks;

    private void Start()
    {
        MusicCore.SetTracksLibrary(tracks);
        Destroy(this);
    }
}
