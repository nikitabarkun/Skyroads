using System.IO;
using UnityEditor;
using UnityEngine;

public class AudioPostprocessor : AssetPostprocessor
{
    private const int MinSizeForStreaming = 5242880;
    
    void OnPreprocessAudio()
    {
        var fileInfo = new FileInfo(assetPath);

        if (fileInfo.Length > MinSizeForStreaming)
        {
            AudioImporter audioImporter = (AudioImporter)assetImporter;
        
            var settings = new AudioImporterSampleSettings();
            settings.loadType = AudioClipLoadType.Streaming;
            
            audioImporter.defaultSampleSettings = settings;
        }
    }
}
