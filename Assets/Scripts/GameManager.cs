using Newtonsoft.Json.Bson;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Control all about score and endlevel
/// </summary>
 //Ensure this initialize before other scripts 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("attempts for the level and points per attempts")]
    [SerializeField] private int attempts;
    [SerializeField] private int pointsPerAttempts;

    [Header("Panels of endgame")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    [Header("Text for panels")]
    [SerializeField] private TextMeshProUGUI pointswin;

    private int score;
    private bool endlevel=false;

    public int Attempts { get => attempts; set => attempts = value; }
    public int Score { get => score; set => score = value; }
    public int PointsPerAttempts { get => pointsPerAttempts; set => pointsPerAttempts = value; }

    private void Awake()
    {
        //create the singleton
        Instance = this;
    }
    private void Update()
    {
        if (attempts<=0 && !endlevel)
        {
            ActivateLosePanel();
        }
    }

    public void ResetGame()
    {
        score = 0;
    }
    public void ActivateWinPanel()
    {
        Time.timeScale = 0;
        panelWin.SetActive(true);
        score = pointsPerAttempts * attempts;
        
        pointswin.text="Score: "+score.ToString("0000");

    }
    private void ActivateLosePanel()
    {
        Time.timeScale=0;
        panelLose.SetActive(true);
    }
    public void nextLevel()
    {
        //total number of levels
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        CheckRecordPoints(currentLevel);
        switch (currentLevel)
        {
            case 1:
                SceneManager.LoadScene("Level2");
            break;
            case 2:
                SceneManager.LoadScene("Level3");
            break;
        }
    }
    public void PlayAgain()
    {
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        Time.timeScale = 1;
        endlevel=true;
        CheckRecordPoints(currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void CheckRecordPoints(int level)
    {
        string key = "HighScore_Level" + level;
        int highScore = PlayerPrefs.GetInt(key, 0000);//Put value 0 if not exist a value
        if (score > highScore)
        {
            PlayerPrefs.SetInt(key, score);
        }
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

}
