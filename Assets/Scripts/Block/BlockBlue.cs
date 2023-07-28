using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBlue : Block
{
    [SerializeField] private bool _startActivated = true;
    [SerializeField] private float _impulseMultiplier = 1;
    [SerializeField] private AudioSource _activateSound;
    [SerializeField] private static float _baseJumpHeight = 5;

    static private float _baseImpulse;
    static private int _greenLayer;
    private bool _isActivated = false;

    protected override void Start()
    {
        base.Start();
        _baseImpulse = Mathf.Sqrt(2 * Physics.gravity.magnitude * _baseJumpHeight);
        _greenLayer = LayerMask.NameToLayer("Green");

        if (_startActivated)
            Move(true);
    }
    public override void Interact(bool activate)
    {
        // When interacting with blue blocks, only deactivation works
        if (_isActivated && !activate && _blockMovement.IsMoveableToTarget(0))
            Move(false);
    }

    private void Move(bool activate)
    {
        int target = activate ? 1 : 0;
        _blockMovement.MoveToTarget(target);
        _isActivated = !_isActivated;
    }

    public bool TryActivate()
    {
        if (!_isActivated && _blockMovement.IsMoveableToTarget(1))
        {
            Move(true);
            _activateSound.Play();
            return true;
        }
        return false;
    }

    public Vector3 GetImpulse()
    {
        return -transform.up
            * _baseImpulse * _impulseMultiplier;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == _greenLayer && TryActivate())
        {
            collision.gameObject.GetComponent<Rigidbody>()
                .AddForce(GetImpulse(), ForceMode.VelocityChange);
        }
    }
}
