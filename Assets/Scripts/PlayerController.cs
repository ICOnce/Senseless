using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    InputActionMap map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = new InputActionMap();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * moveAction.ReadValue<float>();
        RaycastHit2D rayHit;

        if (rayHit = Physics2D.Raycast(transform.position, new Vector2(0 ,-1), 0.17f))
        {

            if (jumpAction.triggered)
            {
                rb.linearVelocityY = 2f;
            }
            Debug.DrawRay(transform.position, -transform.up * rayHit.distance, Color.red);
            Debug.Log("hit");
        }
    }
}
