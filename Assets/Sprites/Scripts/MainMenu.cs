using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private TMP_Text bestTimeText;

    void Start()
    {
        Time.timeScale = 1f;

        if (PlayerPrefs.HasKey("BestTime"))
            bestTimeText.text = $"Kỷ lục: {PlayerPrefs.GetFloat("BestTime"):0.0}s";
        else
            bestTimeText.text = "Chưa có kỷ lục";
    }

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}