using UnityEngine;

/// <summary>
/// Keeps the game state (score and total kegels) across scenes
/// Singleton pattern + DontDestroyOnLoad so can be read in other scenes
/// </summary>
[DefaultExecutionOrder(-100)] //Ensure this initialize before other scripts 
public class GameManager : MonoBehaviour
{
    //Singleton public read-only
    public static GameManager Instance { get; private set; }

    private int score;
    private int kegelNumber;

    //public Read-only accesors
    [HideInInspector]
    public int Score => score;

    [HideInInspector]
    public int KegelNumber => kegelNumber;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //There is already one, remove duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); //Persist between scenes

    }

    /// <summary>
    /// Sets the total number of Kegels at the start of the level
    /// </summary>
    /// <param name="total"></param>
    public void SetKegelNumber(int total)
    {
        kegelNumber = total;
    }

    /// <summary>
    /// Sets the score, used when the kegel enters in the win box area
    /// </summary>
    /// <param name="value"></param>
    public void SetScore(int value)
    {
        score = value;
    }

    /// <summary>
    /// Reset the game values
    /// </summary>
    public void ResetGame()
    {
        score = 0;
        kegelNumber = 0;
    }


}
