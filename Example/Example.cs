using UnityEngine;

public class ExampleSript : MonoBehaviour
{
    private void Start()
    {
        MusicCore.Play("main");
    }

    public void Music(string name)
    {
        MusicCore.Play(name);
    }
}
