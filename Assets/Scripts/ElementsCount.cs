using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Activate when the ball pass the goalkeeper
/// </summary>
public class ElementsCount : MonoBehaviour
{
    //Called when a collider enters the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GameManager.Instance.ActivateWinPanel();
        }
    }

}
