using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float MoveSpeed = 2;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private float _delayTimer = 0;
    private float _moveSpeedModifier = 1;
    private Rigidbody _rigidbody;
    private LayerMask _interactionLayer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = _rigidbody.position;
        _interactionLayer = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMoving)
        {
            if (_delayTimer > 0)
                _delayTimer -= Time.deltaTime;
            else
            {
                Vector3 movement = Vector3.MoveTowards(_rigidbody.position
                    , _targetPosition, MoveSpeed * _moveSpeedModifier * Time.deltaTime);
                _rigidbody.MovePosition(movement);
                if (_rigidbody.position == _targetPosition)
                    _isMoving = false;
            }
        }
    }

    public void MoveToTarget(int target, float delay = 0, float moveSpeedModifier = 1)
    {
        _isMoving = true;
        _delayTimer = delay;
        _moveSpeedModifier = moveSpeedModifier;
        _targetPosition = _startPosition + target * -transform.up;
    }

    public bool IsMoveableToTarget(int target)
    {
        return !_isMoving && !IsObstructionAhead(target);
    }

    private bool IsObstructionAhead(int target)
    {
        Vector3 targetPosition = _startPosition - target * transform.up;
        Vector3 direction = targetPosition - _rigidbody.position;
        //Debug.DrawRay(_rigidbody.position, direction, Color.magenta, 3, false);
        if (Physics.Raycast(_rigidbody.position, direction, out RaycastHit hitInfo, direction.magnitude, _interactionLayer))
        {
            return true;
        }
        return false;
    }
}
