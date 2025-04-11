using System.IO;
using System.Runtime.CompilerServices;
using Oculus.Platform;
using UnityEngine;

public class RecordButton : MonoBehaviour
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

    // Start recording audio
    public void Record()
    {
        playbackEngine.audioSource = GetComponent<AudioSource>();
        playbackEngine.audioSource.clip = Microphone.Start(playbackEngine.microphone, true, 10, 44100);
        playbackEngine.audioSource.Play();

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
