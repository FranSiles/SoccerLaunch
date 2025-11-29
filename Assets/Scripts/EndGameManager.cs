using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Show the result message in the end scene using data from GameManager
/// </summary>
public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textResult;

    private void Start()
    {
        string finalMessage = $"SCORE : {GameManager.Instance.Score}";

        finalMessage += GameManager.Instance.Score  + "\n\nYOU WIN!!!";

        textResult.text = finalMessage;
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Game");
    }


}
