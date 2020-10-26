using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highscoreText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text timePassedText;
    [SerializeField]
    private Text asteroidsPassedText;

    [SerializeField]
    private GameObject damageTakenPanel;
    private Animation damageTakenPanelAnimation;

    public void OnStart()
    {
        SetScore(0);
        SetHighscore(PlayerPrefs.GetInt("Highscore"));
        SetHealth(GameController.HealthCount);
        SetTime(0);
        SetPassedAsteroids(0);

        damageTakenPanelAnimation = damageTakenPanel.GetComponent<Animation>();
    }

    public void SetActive(bool value)
    {
        mainCanvas.gameObject.SetActive(value);
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void SetHighscore(int highscore)
    {
        highscoreText.text = $"Best: {highscore}";
    }

    public void SetHealth(int health)
    {
        if (health > 0)
        {
            healthText.text = $"+{health}";
        }
        else
        {
            healthText.text = "0";
        }
    }

    public void SetTime(int time)
    {
        timePassedText.text = time.ToString();
    }

    public void SetPassedAsteroids(int count)
    {
        asteroidsPassedText.text = count.ToString();
    }

    public void PlayDamageTakenAnimation()
    {
        damageTakenPanelAnimation.Play("DamageTaken");
    }
}
