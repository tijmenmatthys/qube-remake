using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform[] _checkpoints;
    [SerializeField] private Transform _offset;

    private int _current = 0;
    private CharacterController _playerCharController;

    void Awake()
    {
        _playerCharController = _player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TeleportBackward")) Teleport(false);
        else if (Input.GetButtonDown("TeleportForward")) Teleport(true);
    }

    private void Teleport(bool forward)
    {
        if (forward) _current++;
        else _current--;
        _current = Mathf.Clamp(_current, 0, _checkpoints.Length - 1);

        _playerCharController.enabled = false;
        _player.transform.position = _checkpoints[_current].transform.position;
        _player.transform.localPosition += _offset.localPosition;
        _player.transform.rotation = _checkpoints[_current].transform.rotation;
        _playerCharController.enabled = true;
    }

}
