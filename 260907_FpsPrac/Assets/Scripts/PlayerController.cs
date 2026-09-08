using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    [SerializeField] private KeyCode _interactionKey =  KeyCode.E;
    
    private PlayerWeapon _weapon;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    
    private IInteractable _targetInteractable;
    
    private bool _hasDetectInteractable => _targetInteractable != null;
    private bool _isPressedInteractionKey => Input.GetKeyDown(_interactionKey);
    private bool _canInteraction => _hasDetectInteractable && _isPressedInteractionKey;
    
    public GameObject GameObject { get => gameObject; }

    private void Awake() => CacheComponents();
    
    private void Start() => LockCursor();

    private void FixedUpdate() => _movement.Move();
    
    private void Update()
    {
        _movement.Rotate();
        _weapon.Fire();
        _weapon.Reload();
        DetectInteractable();
        TryInteract();
    }

    private void LateUpdate()
    {
        SetCameraTransform();
        SetWeaponTransform();
    }

    private void CacheComponents()
    {
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _cameraTransform = Camera.main.transform;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;     // 마우스 커서 보임 여부
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
        
        // _cameraTransform.position = _cameraPivot.position;
        // _cameraTransform.rotation = _cameraPivot.rotation;
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _detectionRange))
        {
            if (_hasDetectInteractable)
            {
                _targetInteractable.UnTargeting();
                _targetInteractable = null;
            }

            return;
        }
        
        if (_hasDetectInteractable)
        {
            if (hit.collider.gameObject == _targetInteractable.GameObject)
            {
                return; // 같은 Interactable을 계속 주시하고 있는 경우
            }
        }
        
        _targetInteractable?.UnTargeting();
        _targetInteractable = hit.collider.GetComponent<IInteractable>();
        
        // if(_hasDetectInteractable) _targetInteractable.Targeting();
        _targetInteractable?.Targeting(); // ? : _targetInteractable이 null이 아니면 실행안함
    }

    public void TryInteract()
    {
        if (!_canInteraction) return;
        
        _targetInteractable.Interact(this);
        _movement._isSteamPack = true;
        _targetInteractable = null;
    }
}
