using Oculus.Platform;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    DawPlaybackEngine playbackEngine; // Reference to the DawPlaybackEngine script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        playbackEngine.audioSource.Play();
    }
}
