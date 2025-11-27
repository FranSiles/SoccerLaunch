using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Counts how many kegels are inside this trigger area and reports it to the GameManager
/// Requires a 2D trigger collider 
/// </summary>
public class ElementsCount : MonoBehaviour
{
    //Called when a collider enters the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GoToEndScene();
        }
    }
    private void GoToEndScene()
    {
        SceneManager.LoadScene("EndGame");        
    }

}
