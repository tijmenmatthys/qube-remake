using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _amount = 0.1f;
    [SerializeField] private float _speed = 1f;

    private Vector3 _targetPos;
    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
        _targetPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, _targetPos, _speed * Time.deltaTime);
        if (transform.localPosition == _targetPos)
            _targetPos = _startPos;
    }

    public void PushIn()
    {
        _targetPos = _startPos + _amount * Vector3.forward;
    }

    public void PushOut()
    {
        _targetPos = _startPos - _amount * Vector3.forward;
    }
}
