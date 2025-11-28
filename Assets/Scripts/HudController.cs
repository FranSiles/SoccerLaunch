using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [Header("Text of attempts")]
    [SerializeField] TextMeshProUGUI textAttempts;

    
    
    private void Start()
    {
        textAttempts.text = "Attempts:"+GameManager.Instance.Attempts.ToString("00");
    }
    private void Update()
    {
        textAttempts.text = "Attempts:" + GameManager.Instance.Attempts.ToString("00");
    }

}
