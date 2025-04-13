using UnityEngine;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using System.IO.Pipes;

//DawPlaybackEngine playbackEngine;

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

    private static void ByteArrayGroup(FileStream fileStream, AudioClip clip, int samples, int channels, int hz)
    {
        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        fileStream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);
    } // Write the header information to the file

    private static void WriteHeader(FileStream fileStream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin); // Go back to the beginning of the file
        ByteArrayGroup(fileStream, clip, samples, channels, hz); // Write the header information to the file
    }
}
