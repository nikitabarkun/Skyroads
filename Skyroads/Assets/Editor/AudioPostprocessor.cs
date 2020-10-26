using System.IO;
using UnityEditor;
using UnityEngine;

public class AudioPostprocessor : AssetPostprocessor
{
    private const int MinSizeForCompressedInMemory = 204800;
    private const int MinSizeForStreaming = 5242880;
    
    void OnPreprocessAudio()
    {
        var fileInfo = new FileInfo(assetPath);

        AudioImporter audioImporter = (AudioImporter)assetImporter;
        
        var settings = new AudioImporterSampleSettings();
        
        if (fileInfo.Length > MinSizeForStreaming)
        {
            settings.loadType = AudioClipLoadType.Streaming;
        }
        else if (fileInfo.Length > MinSizeForCompressedInMemory)
        {
            settings.loadType = AudioClipLoadType.CompressedInMemory;
        }
        else
        {
            settings.loadType = AudioClipLoadType.DecompressOnLoad;
        }
        
        audioImporter.defaultSampleSettings = settings;
    }
}
