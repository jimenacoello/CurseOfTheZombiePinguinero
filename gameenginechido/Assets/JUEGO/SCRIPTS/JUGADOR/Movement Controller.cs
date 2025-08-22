using UnityEngine;
using UnityEngine.InputSystem.XInput;

[RequireComponent(typeof(CheckGround), typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    // Velocidades fijas para cada estado
    private readonly float crouchSpeed = 3f;
    private readonly float walkSpeed = 5f;
    private readonly float runSpeed = 7f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 6f;

    private Rigidbody rb;
    private CheckGround checkGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        checkGround = GetComponent<CheckGround>();
    }

    private void Update()
    {
        Movement();
        Jump();
        PlayFootsteps();
    }

    // Movimiento principal usando el InputController
    private void Movement()
    {
        Vector3 move = new Vector3(
            Input_Controller.Instance.HorizontalMovement() * ActualSpeed(),
            rb.linearVelocity.y, 
            Input_Controller.Instance.VerticalMovement() * ActualSpeed()
        );

        rb.linearVelocity = transform.rotation * move; // Igual, usamos linearVelocity
    }

    // Determinar la velocidad real según el input
    private float ActualSpeed()
    {
        return Input_Controller.Instance.RunInput()
            ? runSpeed
            : walkSpeed;
    }



    private void Jump()
    {
        if (Input_Controller.Instance.JumpInput() && checkGround.IsTouchingGround())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Aplica la fuerza del salto
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    private void PlayFootsteps()
    {
        bool isMoving = (Mathf.Abs(Input_Controller.Instance.HorizontalMovement()) > 0.01f
                        || Mathf.Abs(Input_Controller.Instance.VerticalMovement()) > 0.01f)
                        && checkGround.IsTouchingGround();

        AudioManager.Instance.PlayPlayerFootsteps(isMoving);
    }
}
