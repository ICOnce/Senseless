using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;

    public float speed = 1;

    public float rayDistance = 0.52f;
    public LayerMask groundLayer;
    Vector2 checkBoxSize;
    bool isGrounded = false;
    bool jumpPressed = false;

    public Animator animator;

    public float JumpHeight;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkBoxSize.x = GetComponent<CapsuleCollider2D>().size.x;
        checkBoxSize.y = 0.1f;
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;


        velocity.x = moveAction.ReadValue<float>() * speed;

        animator.SetBool("IsMoving", moveAction.IsPressed());

        float move = moveAction.ReadValue<float>();

        if (move != 0)
        {
            GetComponent<SpriteRenderer>().flipX = move < 0;
        }

        CheckGround();
        if (isGrounded)
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);

            if (jumpPressed)
            {
                velocity.y = JumpHeight;
                isGrounded = false;
                animator.SetBool("Jumping", true);
            }
        }

        jumpPressed = false;
        rb.linearVelocity = velocity;
        if (velocity.y < -0.1f)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
    }

    void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(transform.position, checkBoxSize, 0f, Vector2.down, rayDistance * transform.localScale.y, groundLayer);
        if (rayHit.collider != null)
        {
            isGrounded = true;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.down * rayDistance, checkBoxSize);
    }
}
