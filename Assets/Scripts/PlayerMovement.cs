using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float maxGravity;
    [SerializeField]
    private float gravityInc;
    private float gravity;
    [SerializeField]
    private float speedAcl = 10f;
    [SerializeField]
    private float speedDecel = 20f;
    private float currentSpeed;
    private float currentYVel;
    private Vector2 movementVector = Vector2.zero;
    private Rigidbody rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    [Header("Animations")]
    [SerializeField]
    private Animator animator;
    private int velocityHash;

    [Header("Sound")]
    private float maxStepTimer = 0.5f;
    private float stepTimer = 0;

    private Bud budRef;

    private void Awake(){
        rb = GetComponent<Rigidbody>();

        currentSpeed = 0;

        velocityHash = Animator.StringToHash("velocity");

        budRef = GetComponent<Bud>();

        if(!budRef.GetisRootBud()) {
            speed += Random.Range(-1, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movementVector.magnitude > 0) {
            currentSpeed += (speedAcl * Time.deltaTime);
            if(currentSpeed > speed) 
                currentSpeed = speed;
        } else {
            if(currentSpeed > 0) {
                currentSpeed -= (speedDecel * Time.deltaTime);
                if(currentSpeed < 0)
                    currentSpeed = 0;
            }
        }

        Vector3 lookTo = new Vector3(movementVector.x, transform.rotation.y, movementVector.y);
        transform.forward = Vector3.Lerp(transform.forward, lookTo, 0.01f);

        animator.SetFloat(velocityHash, (currentSpeed / speed));

        if(IsGrounded()) {
            rb.velocity = new Vector3(movementVector.x, 0, movementVector.y) * currentSpeed;
            gravity = 0;
        } else {
            gravity += gravityInc * Time.deltaTime;
            if(gravity > maxGravity) {
                gravity = maxGravity;
            }
            rb.velocity = new Vector3(movementVector.x * currentSpeed, -gravity, movementVector.y * currentSpeed);
        }
    }

    public void Move(InputAction.CallbackContext context) {
        movementVector = context.ReadValue<Vector2>();
    }

    public Vector2 GetInputRef() {
        return movementVector;
    }

    private void ApplyGravity() {
        currentYVel = rb.velocity.y;
        currentYVel -= gravity * Time.deltaTime;
        if(currentYVel < -maxGravity) {
            currentYVel = -maxGravity;
        }
        rb.velocity = new Vector3(rb.velocity.x, currentYVel, rb.velocity.z);
    }

    private bool IsGrounded() {
        return Physics.Raycast(groundCheck.position, Vector3.down, 0.5f, groundLayer);
    }

    public void AddForce(Vector3 force) {
        rb.AddForce(force / budRef.GetCamera().GetComponent<CameraController>().GetBudCount(), ForceMode.Force);
    }
}
