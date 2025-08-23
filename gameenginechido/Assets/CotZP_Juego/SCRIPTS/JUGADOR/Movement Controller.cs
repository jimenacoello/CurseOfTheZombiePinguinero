using UnityEngine;
using UnityEngine.InputSystem.XInput;

[RequireComponent(typeof(CheckGround), typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private readonly float walkSpeed = 5f;
    private readonly float runSpeed = 7f;
    private float jumpForce = 6f;

    public Rigidbody rb;
    public CheckGround checkGround;

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

    private void Movement()
    {
        Vector3 move = new Vector3(
            Input_Controller.Instance.HorizontalMovement() * ActualSpeed(),
            rb.linearVelocity.y, 
            Input_Controller.Instance.VerticalMovement() * ActualSpeed()
        );

        rb.linearVelocity = transform.rotation * move; 
    }

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
