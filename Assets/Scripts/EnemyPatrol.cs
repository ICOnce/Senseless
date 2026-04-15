using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private bool _movingRight = true;
    private bool _hasFlipped;

    public float speed;

    public Transform groundCheckTransform;
    public float groundCheckDistance = 0.5f;

    public Transform wallCheckTransform;
    public float wallCheckDistance = 0.5f;

    public LayerMask collisionLayer;

    void Update()
    {
        // Check if grounded.
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundCheckDistance, collisionLayer);
        // Check for wall.
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheckTransform.position, Vector2.right * Mathf.Sign(speed), wallCheckDistance, collisionLayer);

        // Only move if grounded.
        if (groundInfo.collider != null)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            _hasFlipped = false; // Reset when grounded so we can flip again if needed on the next update.
        }

        // Flip if we are not grounded or if we hit a wall, but only if we haven't already flipped.
        if ((groundInfo.collider == null || wallInfo.collider != null) && !_hasFlipped)
        {
            Flip();
            _hasFlipped = true;
        }
    }

    void Flip()
    {
        _movingRight = !_movingRight;

        // Turn the enemy in the opposite direction.
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        speed *= -1;
    }
}
