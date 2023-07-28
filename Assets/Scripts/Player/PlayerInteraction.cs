using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _aimingLayerMask;
    [SerializeField] private Renderer[] _gloveElements;
    [SerializeField] private Material _defaultGloveMaterial;
    [SerializeField] private Animator _animatorLeft;
    [SerializeField] private Animator _animatorRight;
    [SerializeField] private GameObject _clickMarkPrefabLeft;
    [SerializeField] private GameObject _clickMarkPrefabRight;
    [SerializeField] private AudioSource _activateSound;
    [SerializeField] private AudioSource _deactivateSound;

    private int _interactionLayer;
    private PlayerMovement _playerMovement;
    private CameraMovement _cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        _interactionLayer = LayerMask.NameToLayer("Interactable");
        _playerMovement = GetComponent<PlayerMovement>();
        _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGloves();
    }

    private void UpdateGloves()
    {
        Ray lookRay = _camera.ViewportPointToRay(Vector3.one / 2);
        if (Physics.Raycast(lookRay, out RaycastHit hitInfo, _camera.farClipPlane, _aimingLayerMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.layer == _interactionLayer)
            {
                Block block = hitObject.GetComponent<Block>();
                SetGloveMaterial(block.GetMaterial());
                ApplyMouseClick(hitObject, block, hitInfo.point);
            }
            else
            {
                SetGloveMaterial(_defaultGloveMaterial);
                print(hitObject + " --- " + hitInfo.point);
                ApplyMouseClick(hitObject, null, hitInfo.point);
            }
        }
    }

    private void ApplyMouseClick(GameObject hitObject, Block block, Vector3 hitPosition)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (block) block.Interact(true);
            _animatorLeft.Play("Lift");
            _activateSound.Play();
            _cameraMovement.PushIn();
            Instantiate(_clickMarkPrefabLeft, hitPosition, Quaternion.identity, hitObject.transform);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (block) block.Interact(false);
            _animatorRight.Play("Lift");
            _deactivateSound.Play();
            _cameraMovement.PushOut();
            Instantiate(_clickMarkPrefabRight, hitPosition, Quaternion.identity, hitObject.transform);
        }
    }

    private void SetGloveMaterial(Material material)
    {
        foreach(Renderer renderer in _gloveElements)
        {
            renderer.material = material;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == _interactionLayer)
        {
            BlockBlue blockBlue = hit.gameObject.GetComponent<BlockBlue>();
            if (blockBlue)
            {
                if (blockBlue.TryActivate())
                {
                    _playerMovement.SetBlueImpulse(blockBlue.GetImpulse());
                }
            }
        }
    }
}
