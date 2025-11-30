using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource goal;
    [SerializeField] private AudioSource kickBall;
    [SerializeField] private AudioSource postBall;
    [SerializeField] private AudioSource netFootball;
    [SerializeField] private AudioSource stadiumSound;
    [SerializeField] private AudioSource background;
    [SerializeField] private AudioSource stopBall;


    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public void PlayGoalSound()
    {
        goal.Play();
    }

    public void PlayKicBallSound()
    {
        kickBall.Play();
    }
    public void PlayPostBallSound()
    {
        postBall.Play();
    }
    public void PlayNetFootballSound()
    {
        netFootball.Play();
    }
    public void PlayStadiumSoundPlay()
    {
        stadiumSound.Play();
    }
    public void PlayBackgroundPlay()
    {
        background.Play();
    }
    public void PlayStopBall()
    {
        stopBall.Play();
    }
    public void StopBackground()
    {
        background.Stop();
    }
    public void StopStadium()
    {
        stadiumSound.Stop();
    }
    public void StopGoal()
    {
        goal.Stop();
    }
}
