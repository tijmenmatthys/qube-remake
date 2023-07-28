using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenReset : Block
{
    [SerializeField] private GameObject Green;
    [SerializeField] private Transform GreenSpawner;
    [SerializeField] private Vector3 SpawnOffset = new Vector3(.5f,.5f,-.5f);

    private Rigidbody _greenRigidbody;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _greenRigidbody = Green.GetComponent<Rigidbody>();
    }

    public override void Interact(bool activate)
    {
        if (activate)
        {
            _greenRigidbody.transform.position = GreenSpawner.position + SpawnOffset;
            _greenRigidbody.velocity = Vector3.zero;
        }
    }
}
