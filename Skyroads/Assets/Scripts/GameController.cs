using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent GameStartEvent;
    public UnityEvent GameOverEvent;

    [SerializeField]
    private static int boostSpeedMultiplier = 2;
    private static int currentBoostSpeedMultiplier = 1;

    [SerializeField]
    private static int healthCount = 2;

    [SerializeField]
    private const float boostLenght = 5f;
    [SerializeField]
    private const float boostCooldown = 5f;
    [SerializeField]
    private const int boostScoreMultiplier = 2;

    private bool isHighscoreBeaten = false;

    private bool isGameOver = false;
    private bool isFirstStart = true;

    private int score = -1;
    private int highscore = 0;
    private int timePassed = 0;
    private int asteroidsPassed = 0;

    [SerializeField]
    private MainCanvasController mainCanvasController;
    [SerializeField]
    private StartCanvasController startCanvasController;
    [SerializeField]
    private PauseCanvasController pauseCanvasController;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private AudioSource damageTakenAudioSource;
    [SerializeField]
    private AudioSource boostAudioSource;

    private IEnumerator scoreCounter;

    public static int HealthCount { get => healthCount; }
    public bool IsBoostedAlready { get; private set; } = false;
    public static int CurrentBoostSpeedMultiplier { get => currentBoostSpeedMultiplier; set => currentBoostSpeedMultiplier = value; }

    private void Awake()
    {
        PauseCanvasController.gameSoundsAudioSources.Add(boostAudioSource);
        PauseCanvasController.gameSoundsAudioSources.Add(damageTakenAudioSource);
    }

    private void Start()
    {
        StopTime(true);
    }

    private void Update()
    {
        if (isFirstStart)
        {
            if (Input.anyKey) 
            {
                StartGame();
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void StartGame()
    {
        isGameOver = false;
        isFirstStart = false;
        scoreCounter = CountTimeAndScore();
        StartCoroutine(scoreCounter);

        highscore = PlayerPrefs.GetInt("Highscore");
        isHighscoreBeaten = false;

        healthCount = 2;
        score = -1;
        timePassed = 0;
        asteroidsPassed = 0;

        currentBoostSpeedMultiplier = 1;

        mainCanvasController.SetActive(true);
        startCanvasController.SetActive(false);
        
        StopTime(false);

        GameStartEvent.Invoke();
    }
    
    public void Unpause()
    {
        StopTime(false);
        
        pauseCanvasController.SetActive(false);
        mainCanvasController.SetActive(true);
    }
    
    public void Boost()
    {
        StartCoroutine(BoostCoroutine());
    }


    private static void StopTime(bool value)
    {
        Time.timeScale = value == true ? 0 : 1;
    }

    private void Pause()
    {
        StopTime(true);
        
        pauseCanvasController.SetActive(true);
        mainCanvasController.SetActive(false);
    }

    private void GameOver()
    {
        isGameOver = true;
        StopCoroutine(scoreCounter);

        mainCanvasController.SetActive(false);
        startCanvasController.SetActive(true);

        if (isHighscoreBeaten)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }

        GameOverEvent.Invoke();

        StopTime(true);
    }

    private IEnumerator BoostCoroutine()
    {
        IsBoostedAlready = true;
        boostAudioSource.Play();

        int previousBoostMultiplier = CurrentBoostSpeedMultiplier;
        CurrentBoostSpeedMultiplier = boostSpeedMultiplier;
        cameraController.SetAnimatorState("isBoosted", true);
        yield return new WaitForSeconds(boostLenght);

        CurrentBoostSpeedMultiplier = previousBoostMultiplier;
        cameraController.SetAnimatorState("isBoosted", false);
        yield return new WaitForSeconds(boostCooldown);

        IsBoostedAlready = false;
    }

    public void AsteroidPassed()
    {
        isHighscoreBeaten = ChangeScore(5);
        asteroidsPassed++;

        mainCanvasController.SetPassedAsteroids(asteroidsPassed);
        startCanvasController.SetPassedAsteroids(asteroidsPassed);
    }

    public void DamageTaken()
    {
        healthCount--;
        damageTakenAudioSource.Play();

        ChangeScore(-5);
        asteroidsPassed--;

        mainCanvasController.SetHealth(healthCount);
        mainCanvasController.PlayDamageTakenAnimation();

        if (HealthCount == 0)
        {
            GameOver();
        }
    }

    private bool ChangeScore(int count)
    {
        score = CurrentBoostSpeedMultiplier > 1 ? score += count * boostScoreMultiplier : score += count;
        score = score < 0 ? 0 : score;

        mainCanvasController.SetScore(score);
        startCanvasController.SetScore(score);

        if (score > highscore)
        {
            highscore = score;

            startCanvasController.SetHighscoreActiveValue(true);
            mainCanvasController.SetHighscore(score);

            return true;
        }
        return false;
    }

    private IEnumerator CountTimeAndScore()
    {
        while (!isGameOver)
        {
            timePassed++;
            isHighscoreBeaten = ChangeScore(1);

            mainCanvasController.SetTime(timePassed);
            startCanvasController.SetTime(timePassed);

            yield return new WaitForSeconds(1f);
        }
    }
}
