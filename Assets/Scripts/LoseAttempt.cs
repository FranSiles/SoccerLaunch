using UnityEngine;

public class LoseAttempt : MonoBehaviour
{
    private Collider2D ballCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ballCollider = collision;
            Invoke("Lose",0.2f);
            
        }
    }
    private void Lose()
    {
        GameManager.Instance.Attempts--;
        ballCollider.GetComponent<LaunchManager>().ResetBall();
    }
}
