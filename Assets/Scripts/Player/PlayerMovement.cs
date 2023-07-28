using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private AudioSource _walkSound, _landingSound;

    private CharacterController _charCtrl;
    private PlayerWalkingSimulation _walkingSim;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _blueImpulse = Vector3.zero;
    private bool _isLaunchedByBlue = false;
    private float _jumpImpulse;
    private bool _isJumpPressed = false;
    private Vector2 _input = Vector2.zero;
    private bool _wasGrounded = true;
    private bool _wasWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        _charCtrl = GetComponent<CharacterController>();
        _walkingSim = GetComponent<PlayerWalkingSimulation>();
        _jumpImpulse = Mathf.Sqrt(2 * Physics.gravity.magnitude * _jumpHeight);
    }

    private void Update()
    {
        // save inputs, to be used in FixedUpdate
        if (Input.GetButtonDown("Jump")) _isJumpPressed = true;
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        PlayWalkingSound();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyInputs();
        ApplyJump();
        ApplyBlueImpulse();
        CheckLanding();
        _charCtrl.Move(_velocity * Time.deltaTime);
    }

    private void PlayWalkingSound()
    {
        bool isWalking = _input != Vector2.zero;
        if (isWalking && !_wasWalking) _walkSound.Play();
        if (!isWalking && _wasWalking) _walkSound.Stop();
        _wasWalking = isWalking;
    }

    private void CheckLanding()
    {
        if (_charCtrl.isGrounded && !_wasGrounded)
        {
            _landingSound.Play();
        }
        _wasGrounded = _charCtrl.isGrounded;
    }

    public void SetBlueImpulse(Vector3 impulse)
    {
        _blueImpulse = impulse;
    }

    private void ApplyBlueImpulse()
    {
        if (_blueImpulse != Vector3.zero)
        {
            _velocity += _blueImpulse;
            _blueImpulse = Vector3.zero;
            _isLaunchedByBlue = true;
        }
    }

    private void ApplyJump()
    {
        if (_isJumpPressed && _charCtrl.isGrounded)
        {
            _velocity.y += _jumpImpulse;
        }
        _isJumpPressed = false;
    }

    private void ApplyGravity()
    {
        if (_charCtrl.isGrounded)
            _velocity.y = -2 * _charCtrl.skinWidth;
        else
            _velocity.y -= Physics.gravity.magnitude * Time.deltaTime;
    }

    private void ApplyInputs()
    {
        if (_isLaunchedByBlue && _charCtrl.isGrounded) _isLaunchedByBlue = false;
        if (!_isLaunchedByBlue)
        {
            _velocity.x = 0;
            _velocity.z = 0;
            Vector3 moveVector = transform.right * _input.x
                             + transform.forward * _input.y;
            _velocity += moveVector * _moveSpeed;
            if (_walkingSim) _walkingSim.Walk(moveVector * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + _velocity);
    }
}
