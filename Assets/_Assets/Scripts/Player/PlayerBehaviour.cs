using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private bool CanMove = true;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isRunning = false;

    [SerializeField] private float _walkingSpeed = 7.5f;
    [SerializeField] private float _runningSpeed = 11f;

    [SerializeField] private float _jumpingSpeed = 8f;
    
    [SerializeField] private float _rotationSpeed = 100f;

    [Header("Grabity Physics")]
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundLayerMask = 6;
    [SerializeField] private float _gravity = 100f;

    [Header ("Camera Perspective")]
    [SerializeField] private Camera _playerCamera;
    private float _limitXRotation = 55f;

    //private CharacterController _characterController;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        //_characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor in center
    }

    private void FixedUpdate()
    {
        RotatePlayer();
        CheckMovement();

        ApplyGravity();
    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseX * _rotationSpeed * Time.deltaTime);
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //_isRunning = true;
            MovePlayer(_runningSpeed);
        }
        else
        {
            //_isRunning = false;
            MovePlayer(_walkingSpeed);
        }

        HandleJump();
    }

    private void MovePlayer(float speed)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);


        forward *= Input.GetAxis("Vertical") * speed;
        right *= Input.GetAxis("Horizontal") * speed;

        _moveDirection = forward + right;

        _rigidbody.velocity = _moveDirection;
        //_characterController.Move(moveDirection * Time.deltaTime); // not the best
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpingSpeed, _rigidbody.velocity.z);
        }
    }

    private void ApplyGravity()
    {
        _rigidbody.AddForce(new Vector3(0, -1.0f, 0) * _rigidbody.mass * _gravity);
    }

    private bool IsGrounded()
    {
        Vector3 rayStart = transform.position;

        return Physics.SphereCast(
            rayStart,
            0.3f,
            Vector3.down,
            out RaycastHit hit,
            _groundCheckDistance,
            _groundLayerMask
        );
    }

    private void MoveAxis(float speed) // clamped to straight axis movement, no rotation
    {
        Vector3 newPosition = transform.position;

        newPosition.z += Input.GetAxis("Vertical") * speed;
        newPosition.x += Input.GetAxis("Horizontal") * speed;

        transform.position = newPosition;
    }
}
