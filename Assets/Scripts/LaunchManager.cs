using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/// <summary>
/// Lets the player drag the ball from a pivot using a SpringJoint2D and then release it
/// </summary>
public class LaunchManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("Delay before disabling the spring after releasing the ball")]
    private float disableStringJointTime = 0.15f;
    [SerializeField, Tooltip("Delay before moving to the end scene after the spring is cut")]
    private float goToEndSceneDelay = 3.5f;

    private Camera mainCamera;
    private Rigidbody2D ballRb;
    private SpringJoint2D springJoint2D;

    private bool isDragging;


    private void Start()
    {
        mainCamera = Camera.main;

        ballRb = GetComponent<Rigidbody2D>();
        springJoint2D = GetComponent<SpringJoint2D>();

    }

    private void Update()
    {
        //if the ball has already been launched, stop processing drag logic
        if (ballRb == null) return;

        //If there is no current press, release it we were dragging
        if (!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            if (isDragging) LaunchBall();
            isDragging = false;
            return;
        }

        //start/continue dragging: take physics control
        isDragging = true;
        ballRb.bodyType = RigidbodyType2D.Kinematic;

        //Read point/touch position, convert to world, keep z=0 for 2D
        Vector2 screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        //Move the rigidbody to follow the finger/mouse
        ballRb.position = worldPos;
    }

    /// <summary>
    /// Release the ball so physics take over and schedules spring cut
    /// </summary>
    private void LaunchBall()
    {
        ballRb.bodyType = RigidbodyType2D.Dynamic; //React to physics again
        ballRb = null;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"),LayerMask.NameToLayer("Ground"),true);
        //Cut the spring after a small delay so the joint has time to apply force
        Invoke(nameof(CutSpringJoint), disableStringJointTime);

    }
    /// <summary>
    /// Disable / Cut the spring Joint and schedules the end scene
    /// </summary>
    private void CutSpringJoint()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("Ground"), false);
        if (springJoint2D != null)
        {
            springJoint2D.enabled = false; //Cut the connection with the pivot
            springJoint2D = null;
        }

        //Go to the end scene after the ball had time to interact
        //Invoke(nameof(GoToEndScene), goToEndSceneDelay);

    }

    /*private void GoToEndScene()
    {
        SceneManager.LoadScene("EndGame");        
    }*/

}
