using System.IO;
using System.Runtime.CompilerServices;
using Oculus.Platform;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    DawPlaybackEngine playback;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!Directory.Exists(playback.directoryPath))
        {
            Directory.CreateDirectory(playback.directoryPath); // Create the directory if it doesn't exist
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start recording audio
    public void Record()
    {
        playback.audioSource = GetComponent<AudioSource>();
        int lengthSec = 3599; // Length of the recording in seconds

        playback.audioSource.clip = Microphone.Start(playback.inputDevice, false, lengthSec, playback.sampleRate);
        playback.startTime = Time.realtimeSinceStartup; // Get the current time

        //FileWrite
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        // 'data' contains the audio samples
        // 'channels' is the number of audio channels (e.g., 2 for stereo)

        channels = 2; // Set the number of channels to 2 (stereo)

        // Process the audio data here
        for (int i = 0; i < data.Length; i++)
        {

        }
    }
}
