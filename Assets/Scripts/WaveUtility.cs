using UnityEngine;
using System.IO;
using System;

DawPlaybackEngine playbackEngine;

public static class WaveUtility
{
    const int headerSize = 44;

    private static FileStream CreateEmpty(string filePath)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create); // Create a new file
        byte emptyByte = new byte(); // Initialise an empty byte
        for (int i = 0; i < headerSize; i++)
        {
            fileStream.WriteByte(emptyByte); // Pre-allocate 44 bytes for the header
        }
        return fileStream;
    }

    public static void Save(string filePath, AudioClip clip)
    {
        if (!filePath.ToLower().EndsWith(".wav"))
        {
            filePath += ".wav"; // Append .wav extension if not present
        }

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Create directory if it doesn't exist

        using (FileStream fileStream = CreateEmpty(filePath))
        {
            ConvertAndWrite(fileStream, clip);
            WriteHeader(fileStream, clip);
        }
    }

    private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels]; // Get the audio samples
        clip.GetData(samples, 0); // Get the audio data from the clip

        Int16[] intData = new Int16[samples.Length];
        byte[] bytesData = new byte[samples.Length * 2]; // 2 bytes per sample

        const float rescaleFactor = 32767; // Rescale factor for 16-bit PCM
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor); // Convert to Int16
            Byte[] byteArray = new Byte[2];
            byteArray = BitConverter.GetBytes(intData[i]); // Convert to byte array
            byteArray.CopyTo(bytesData, i * 2); // Copy to the byte array
        }
        fileStream.Write(bytesData, 0, bytesData.Length); // Write the data to the file
    }
}
