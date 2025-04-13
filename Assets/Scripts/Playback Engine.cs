using UnityEngine;
using UnityEngine.Audio;

public class DawPlaybackEngine : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    public string inputDevice;
    public int sampleRate = 48000; // Default sample rate

    //RecordButton recordButton;
    [HideInInspector] public string filePath = "Audio_Recording.wav"; // Path to save the recorded audio
    [HideInInspector] public string directoryPath = "Recordings"; // Directory to save the recordings
    [HideInInspector] public float startTime;
    [HideInInspector] public float recordingLength;

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
