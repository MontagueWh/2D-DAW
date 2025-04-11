using UnityEngine;
using UnityEngine.Audio;

public class DawPlaybackEngine : MonoBehaviour
{
    public AudioSource audioSource;
    public string microphone;
    public int sampleRate = 48000; // Default sample rate
    RecordButton recordButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseAudio();
    }

    // Initialise audio settings
    private void InitialiseAudio()
    {
        audioSource = GetComponent<AudioSource>();
        microphone = Microphone.devices[0]; // Use the built-in microphone
    }

    private void MonitorInput()
    {
        audioSource.clip = Microphone.Start(microphone, true, 1, sampleRate);
        audioSource.loop = true;

    }
    // Update is called once per frame
    void Update()
    {
    }
}
