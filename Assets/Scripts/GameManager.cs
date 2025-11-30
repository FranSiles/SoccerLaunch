using Newtonsoft.Json.Bson;
using TMPro;
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
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.PlayStadiumSoundPlay();
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
        AudioManager.Instance.StopStadium();
        AudioManager.Instance.PlayBackgroundPlay();
        Time.timeScale = 0;
        panelWin.SetActive(true);
        score = pointsPerAttempts * attempts;
        
        pointswin.text="Score: "+score.ToString("0000");
        pauseButton.SetActive(false);

    }
    private void ActivateLosePanel()
    {
        AudioManager.Instance.StopStadium();
        AudioManager.Instance.PlayBackgroundPlay();
        Time.timeScale=0;
        panelLose.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void nextLevel()
    {
        AudioManager.Instance.StopGoal();
        CheckMaxLevel();
        //total number of levels
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        CheckRecordPoints(currentLevel);
        Time.timeScale = 1;
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
        CheckMaxLevel();
        AudioManager.Instance.StopGoal();
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        Time.timeScale = 1;
        endlevel=true;
        CheckRecordPoints(currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        AudioManager.Instance.StopBackground();
        AudioManager.Instance.PlayStadiumSoundPlay();
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void Pause()
    {
        AudioManager.Instance.StopStadium();
        AudioManager.Instance.PlayBackgroundPlay();
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
        int highScore=0;
        switch (level)
        {
            case 1:
                 highScore = PlayerPrefs.GetInt(GameConstants.HIGHSCORELevel1_KEY, 0000);//Put value 0 if not exist a value
            break;
            case 2:
                 highScore = PlayerPrefs.GetInt(GameConstants.HIGHSCORELevel2_KEY, 0000);//Put value 0 if not exist a value
            break;
            case 3:
                highScore = PlayerPrefs.GetInt(GameConstants.HIGHSCORELevel3_KEY, 0000);//Put value 0 if not exist a value
            break;
        }
        if (score > highScore)
        {
            switch (level)
            {
                case 1:
                    PlayerPrefs.SetInt(GameConstants.HIGHSCORELevel1_KEY, score);
                break;
                case 2:
                    PlayerPrefs.SetInt(GameConstants.HIGHSCORELevel2_KEY, score);
                break;
                case 3:
                    PlayerPrefs.SetInt(GameConstants.HIGHSCORELevel3_KEY, score);
                break;
            }
           
        }
    }
    public void SelectLevel()
    {
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        CheckMaxLevel();
        Time.timeScale = 1;
        CheckRecordPoints(currentLevel);
        AudioManager.Instance.StopGoal();
        SceneManager.LoadScene("SelectLevel");
    }

    /// <summary>
    /// Check Max Level
    /// </summary>
    void CheckMaxLevel()
    {
        //ask for the current level
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        //Save the next Level for the first time
        if (!PlayerPrefs.HasKey(GameConstants.MAXLEVEL_KEY))
        {
            PlayerPrefs.SetInt(GameConstants.MAXLEVEL_KEY, currentLevel + 1);
        }
        else
        {
            if (PlayerPrefs.GetInt(GameConstants.MAXLEVEL_KEY) < currentLevel + 1)
            {
                PlayerPrefs.SetInt(GameConstants.MAXLEVEL_KEY, currentLevel + 1);
            }

        }
    }

}
