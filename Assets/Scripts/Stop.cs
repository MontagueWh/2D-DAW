using System.IO;
using Oculus.Platform;
using UnityEditor;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    DawPlaybackEngine playback;

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
        //playbackEngine.audioSource.Stop(); // Stop the audio playback

        Microphone.End(null); // Stop the microphone recording
        playback.recordingLength = Time.realtimeSinceStartup - playback.startTime; // Calculate the recording duration
        playback.audioSource.clip = TrimClip(playback.audioSource.clip, playback.recordingLength); // Trim the audio clip to the recorded length
        SaveRecording(); // Save the recorded audio
    }

    public void SaveRecording()
    {
        if (playback.audioSource.clip != null)
        {
            playback.filePath = Path.Combine(playback.directoryPath, playback.filePath); // Combine directory and file path
            WaveUtility.Save(playback.filePath, playback.audioSource.clip); // Save the audio clip
            Debug.Log("Recording saved as: " + playback.filePath); // Log the save location
        }

        else Debug.Log("No audio clip to save");
    }

    private AudioClip TrimClip(AudioClip clip, float length) // Trim the audio clip to the specified length
    {
        int samples = (int)(clip.frequency * length); // Calculate the number of samples
        float[] data = new float[samples]; // Create an array to hold the audio data
        clip.GetData(data, 0); // Get the audio data from the clip

        AudioClip trimmedClip = AudioClip.Create(clip.name, samples, clip.channels, clip.frequency, false); // Create a new audio clip
        trimmedClip.SetData(data, 0); // Set the audio data for the new clip

        return trimmedClip; // Return the trimmed clip
    }
}
