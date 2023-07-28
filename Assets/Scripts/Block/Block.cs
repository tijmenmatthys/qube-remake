using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all special blocks
/// </summary>
public abstract class Block : MonoBehaviour
{
    protected BlockMovement _blockMovement;
    private Material _material;

    protected virtual void Start()
    {
        _blockMovement = GetComponent<BlockMovement>();
        _material = GetComponent<Renderer>().material;
    }

    public abstract void Interact(bool activate);

    public Material GetMaterial()
    {
        return _material;
    }
}
