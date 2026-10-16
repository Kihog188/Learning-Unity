using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Won, Lost }
    public GameState State { get; private set; } = GameState.Playing;

    [SerializeField] private Health playerHealth;
    [SerializeField] private HUD hud;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winTimeText;

    void OnEnable() { playerHealth.OnDied += Lose; }
    void OnDisable() { playerHealth.OnDied -= Lose; }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null || !kb.escapeKey.wasPressedThisFrame) return;

        if (State == GameState.Playing) Pause();
        else if (State == GameState.Paused) Resume();
    }

    public void Pause()
    {
        State = GameState.Paused;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        State = GameState.Playing;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void Lose()
    {
        State = GameState.Lost;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        if (State != GameState.Playing) return;

        State = GameState.Won;
        Time.timeScale = 0f;

        float time = hud.Elapsed;
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (time < best)
        {
            best = time;
            PlayerPrefs.SetFloat("BestTime", best);
            PlayerPrefs.Save();
        }

        winTimeText.text = $"Time: {time:0.0}s\nRecord: {best:0.0}s";
        winPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}