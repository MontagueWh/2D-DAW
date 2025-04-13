using UnityEngine;
using UnityEngine.Audio;

public class DawPlaybackEngine : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [HideInInspector] public string inputDevice;
    [HideInInspector] public int sampleRate;
    [HideInInspector] public string filePath = "Audio_Recording.wav"; // Path to save the recorded audio
    [HideInInspector] public string directoryPath = "Recordings"; // Directory to save the recordings
    [HideInInspector] public float startTime;
    [HideInInspector] public float recordingLength;
    [HideInInspector] public AudioClip recordedClip;
    [HideInInspector] public int lengthSec = 3599; // Maximum length of the recording (in seconds) in Unity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseAudio();
    }

    // Initialise audio settings
    private void InitialiseAudio()
    {
        audioSource = GetComponent<AudioSource>();
        inputDevice = Microphone.devices[0]; // Use the built-in microphone
        sampleRate = 48000; // Default sample rate
    }

    private void MonitorInput()
    {
        //audioSource.clip = Microphone.Start(inputDevice, true, 1, sampleRate);
        //audioSource.loop = true;

    }
    // Update is called once per frame
    void Update()
    {
    }
}
