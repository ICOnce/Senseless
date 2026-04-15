using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;


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
        checkBoxSize.x = GetComponent<BoxCollider2D>().size.x;
        checkBoxSize.y = 0.1f;
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;


        velocity.x = moveAction.ReadValue<float>();

        animator.SetBool("IsMoving", moveAction.IsPressed());

        CheckGround();
        if (jumpPressed && isGrounded)
        {
            velocity.y = JumpHeight;
            isGrounded = false;
        }
        jumpPressed = false;
        rb.linearVelocity = velocity;
        animator.SetBool("Jumping", !isGrounded);
    }

    void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(transform.position, checkBoxSize, 0f, Vector2.down, rayDistance, groundLayer);
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
