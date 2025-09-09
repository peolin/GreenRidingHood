using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    Idle,
    Walking,
    Running,
    Jumping
}

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerState _currentState;
    /*public PlayerState CurrentState
    {
        get => _currentState;
    }*/

    public static event Action<PlayerState> OnPlayerStateChanged;

    [Header("Player Movement")]
    [SerializeField] private bool CanMove = true;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _wasMoving = false;

    [SerializeField] private float _walkingSpeed = 7.5f;
    [SerializeField] private float _runningSpeed = 11f;

    [SerializeField] private float _jumpingSpeed = 8f;

    [SerializeField] private float _rotationSpeed = 100f;

    [Header("Grabity Physics")]
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundLayerMask = 6;
    [SerializeField] private float _gravity = 100f;

    [Header("Camera Perspective")]
    [SerializeField] private Camera _playerCamera;
    private float _limitXRotation = 55f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _currentState = PlayerState.Idle;
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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        _isMoving = ((horizontal != 0) || (vertical != 0));

        if (_isMoving)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed;
            _currentState = Input.GetKey(KeyCode.LeftShift) ? PlayerState.Running : PlayerState.Walking;

            MovePlayer(speed, vertical, horizontal);
        }
        else if (_wasMoving)
        {
            _currentState = PlayerState.Idle;
            //StopMovement();
        }

        _wasMoving = _isMoving;

        HandleJump();

        OnPlayerStateChanged?.Invoke(_currentState);
    }

    private void MovePlayer(float speed, float vertical, float horizontal)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        forward *= vertical * speed;
        right *= horizontal * speed;

        _moveDirection = forward + right;

        _rigidbody.velocity = _moveDirection;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _currentState = PlayerState.Jumping;

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
}
