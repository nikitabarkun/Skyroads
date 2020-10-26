using UnityEngine;
using UnityEngine.UI;

public class StartCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Canvas startCanvas;
    [SerializeField]
    private GameObject activeOnRestart;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highscoreText;
    [SerializeField]
    private Text timePassedText;
    [SerializeField]
    private Text asteroidsPassedText;

    [SerializeField]
    private Text startText;

    [SerializeField]
    private Text exitText;

    public void OnStart()
    {
        SetHighscoreActiveValue(false);
    }

    public void OnGameOver()
    {
        activeOnRestart.SetActive(true);
        startText.text = "RESTART";
    }

    public void SetActive(bool value)
    {
        startCanvas.gameObject.SetActive(value);
    }

    public void SetScore(int score)
    {
        scoreText.text = $"GAME OVER WITH SCORE: {score}";
    }

    public void SetHighscoreActiveValue(bool value)
    {
        highscoreText.gameObject.SetActive(value);
    }

    public void SetTime(int time)
    {
        timePassedText.text = $"TIME: {time} SECONDS";
    }

    public void SetPassedAsteroids(int count)
    {
        asteroidsPassedText.text = $"ASTEROIDS: {count}";
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnStartButtonClick()
    {
        gameController.StartGame();
    }
}
