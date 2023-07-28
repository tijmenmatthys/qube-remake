using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveBlocks : MonoBehaviour
{
    [SerializeField] private BlockMovement[] _blocks;
    [SerializeField] private int[] _targetPositions;
    [SerializeField] private AudioSource _openSound;

    private LayerMask _greenLayer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_blocks.Length == _targetPositions.Length,
            "Array of target positions must have same length as blocks");
        _greenLayer = LayerMask.NameToLayer("Green");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MoveBlocks();
            _openSound.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _greenLayer)
        {
            Destroy(collision.gameObject);
            MoveBlocks();
        }
    }

    private void MoveBlocks()
    {
        for (int i = 0; i < _blocks.Length; i++)
        {
            _blocks[i].MoveToTarget(_targetPositions[i]);
        }
    }
}
