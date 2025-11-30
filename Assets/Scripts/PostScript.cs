using UnityEngine;

public class PostScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            AudioManager.Instance.PlayPostBallSound();
        }
    }
}
