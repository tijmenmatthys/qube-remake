using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRed : Block
{
    public int MaxPosition = 3;
    public int MinPosition = 0;
    public int StartPosition = 0;

    private int _currentPosition = 0;

    protected override void Start()
    {
        base.Start();
        Move(StartPosition);
    }

    public override void Interact(bool activate)
    {
        if (activate) Move(1);
        else Move(-1);
    }

    private void Move(int amount)
    {
        int newPosition = Mathf.Clamp(_currentPosition + amount, MinPosition, MaxPosition);
        if (_blockMovement.IsMoveableToTarget(newPosition))
        {
            _blockMovement.MoveToTarget(newPosition);
            _currentPosition = newPosition;
        }
    }
}
