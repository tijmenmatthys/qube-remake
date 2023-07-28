using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSimulation : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _amplitude = .1f;
    [SerializeField] private float _frequency = 1f;

    private float _t = 0;

    internal void Walk(Vector3 movement)
    {
        _t += movement.magnitude;
        //float offsetX = Amplitude * Mathf.Sin(Frequency * _t);
        float offsetY = _amplitude * Mathf.Abs(Mathf.Sin(_frequency/2 * _t));
        _body.localPosition = Vector3.up * offsetY;// + Vector3.right * offsetX;
    }
}
