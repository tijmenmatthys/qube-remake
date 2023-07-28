using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;

    private float _rotationX = 0f;

    // Update is called once per frame
    void Update()
    {
        ApplyMouseInput();
    }

    private void ApplyMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _mouseSensitivity;

        _player.Rotate(Vector3.up * mouseX);
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _camera.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }
}
