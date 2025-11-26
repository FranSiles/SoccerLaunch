using UnityEngine;

/// <summary>
/// Counts how many kegels are inside this trigger area and reports it to the GameManager
/// Requires a 2D trigger collider 
/// </summary>
public class ElementsCount : MonoBehaviour
{
    private string kegelTag = "Kegel";

    private int elementNumber = 0;


    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();

        //Initialize the total Kegels in the scene
        /*int totalKegel = GameObject.FindGameObjectsWithTag(kegelTag).Length;
        GameManager.Instance.SetKegelNumber(totalKegel);*/


    }

    //Called when a collider enters the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(kegelTag))
        {
            elementNumber++;
            GameManager.Instance.SetScore(elementNumber);
        }
    }

}
