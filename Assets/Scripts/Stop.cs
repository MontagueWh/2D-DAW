using Oculus.Platform;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    DawPlaybackEngine playbackEngine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Stop recording or playback
    public void Stop()
    {        
        playbackEngine.audioSource.Stop(); // Stop the audio playback
    }
}
