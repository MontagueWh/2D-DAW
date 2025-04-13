using Oculus.Platform;
using UnityEngine;
using System.IO;
using System;

public class PlayButton : MonoBehaviour
{
    DawPlaybackEngine playback; // Reference to the DawPlaybackEngine script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRecording()
    {
        playback.audioSource.clip = playback.recordedClip; // Set the audio source clip to the recorded clip
        playback.audioSource.Play();
    }

    public void StopRecording()
    {
        Microphone.End(null); // Stop the microphone recording
    }
}
