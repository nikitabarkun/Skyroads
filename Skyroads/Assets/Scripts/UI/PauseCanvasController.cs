using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasController : MonoBehaviour
{
    public static List<AudioSource> gameSoundsAudioSources;

    [SerializeField]
    private AudioSource _musicAudioSource;

    [SerializeField] 
    private GameController gameController;
    
    [SerializeField]
    private Canvas pauseCanvas;
    [SerializeField] 
    private Slider gameSoundsSlider;
    [SerializeField] 
    private Slider musicSlider;
    [SerializeField] 
    private Button continueButton;

    private void Awake()
    {
        gameSoundsAudioSources = new List<AudioSource>();
        
        gameSoundsSlider.onValueChanged.AddListener(delegate { OnGameSoundsSliderChanged(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(); });
        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }
    
    public void SetActive(bool value)
    {
        pauseCanvas.gameObject.SetActive(value);
    }

    private void OnGameSoundsSliderChanged()
    {
        foreach (var audioSource in gameSoundsAudioSources)
        {
            audioSource.volume = gameSoundsSlider.value;
        }
    }

    private void OnMusicSliderChanged()
    {
        _musicAudioSource.volume = musicSlider.value;
    }
    
    private void OnContinueButtonClicked()
    {
        gameController.Unpause();
    }
}
