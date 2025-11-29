using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Components
    private Transform checkColision;

    //Ground check
    private LayerMask layer;

    //Movement
    [Header("Enemy movement")]
    [SerializeField] private Vector2 direction;
    [Range(0.5f, 2f)]
    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        checkColision = transform.GetChild(1);
        layer = LayerMask.GetMask("Limit");
    }
    private void Update()
    {
        GoalKeeperMove();
    }
    private void GoalKeeperMove()
    {
        //detects ceil or floor
        if (GeneralDetection(0.5f, direction, Color.magenta))
        {
            direction = -direction;
        }
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
    private bool GeneralDetection(float rayLength, Vector3 direction, Color color)
    {
        Debug.DrawRay(checkColision.position, direction * rayLength, color);
        return Physics2D.Raycast(checkColision.position, direction, rayLength, layer);
    }
}
