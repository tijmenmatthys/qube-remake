using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockYellow : Block
{
    public BlockYellow LeftNeighbour;
    public BlockYellow RightNeighbour;
    public float DelayBetweenBlocks = .3f;

    private bool _isActivated = false;
    private BlockYellow[] _blocks;
    private int _height;

    protected override void Start()
    {
        base.Start();
        InitBlockReferences();
    }
    public override void Interact(bool activate)
    {
        if (activate && !_isActivated || !activate & _isActivated)
        {
            if (activate) SetTargetHeights();
            if (IsAllMoveable(activate))
            {
                MoveAll(activate);
                ToggleIsActivatedAll();
            }
        }
    }

    private void ToggleIsActivatedAll()
    {
        foreach (BlockYellow block in _blocks)
            block._isActivated = !block._isActivated;
    }

    private void MoveAll(bool activate)
    {
        foreach (BlockYellow block in _blocks)
        {
            int target = activate ? block._height : 0;
            float delay = activate ?
                (block._height - 1) * DelayBetweenBlocks :
                (_blocks.Max(x => x._height) - block._height) * DelayBetweenBlocks;
            block._blockMovement.MoveToTarget(target, delay);//, block._height);
        }
    }

    private bool IsAllMoveable(bool activate)
    {
        foreach (BlockYellow block in _blocks)
        {
            int target = activate ? block._height : 0;
            if (!block._blockMovement.IsMoveableToTarget(target))
                return false;
        }
        return true;
    }

    private void SetTargetHeights()
    {
        // clicked block & left neighbours
        BlockYellow current = this;
        int currentHeight = 0;
        while (current)
        {
            current._height = currentHeight;
            current = current.LeftNeighbour;
            currentHeight--;
        }

        // add right neighbours
        current = RightNeighbour;
        currentHeight = -1;
        while (current)
        {
            current._height = currentHeight;
            current = current.RightNeighbour;
            currentHeight--;
        }

        // fix heights to be positive
        int offset = 1 - _blocks.Min(x => x._height);
        foreach (BlockYellow block in _blocks)
            block._height += offset;
    }

    private void InitBlockReferences()
    {
        List<BlockYellow> blocks = new List<BlockYellow>();

        // add this block & left neighbours
        BlockYellow current = this;
        while (current)
        {
            blocks.Add(current);
            current = current.LeftNeighbour;
        }

        // add right neighbours
        current = RightNeighbour;
        while (current)
        {
            blocks.Add(current);
            current = current.RightNeighbour;
        }

        _blocks = blocks.ToArray();
    }
}
