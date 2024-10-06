using UnityEngine;

public class ExampleSript : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Start");
        MusicCore.Play("main");
    }

    public void Music(string name)
    {
        MusicCore.Play(name);
    }

    public void Stop()
    {
        MusicCore.Stop();
    }
}
