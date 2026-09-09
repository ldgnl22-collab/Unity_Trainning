using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUseItem
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private HandBomb _handBomb;

    [SerializeField] private float _steamPackCoolTime = SteamPack._itemCoolTime;

    private KeyCode _throwHandBomb = KeyCode.Space;
    private bool _isThrowHandBomb => Input.GetKeyDown(_throwHandBomb);
    
    private float _coolTime;
    private bool _isTimer;
    
    public float _attackDelay;
    
    private float _pitch;
    private Rigidbody _rigidbody;
    
    public bool _isSteamPack;

    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void Update() => UpCountTimer(_isTimer);

    public void ThrowHandBomb()
    {
        if (!_isThrowHandBomb) return;
        
        _handBomb.UseItem(this);
    }

    private void UpCountTimer(bool _timer)
    {
        if (!_timer) return;
        _coolTime = Time.deltaTime;
        Debug.Log($"{_coolTime} 초 동안 지속중");
    }

    public void SteamPackSetCoolTime(float time)
    {
        if (!(_steamPackCoolTime <= _coolTime))
        {
            _isTimer = false;
            return;
        }
        _weapon.SetAutoFireCoolTime(time);
        _isTimer = true;
    }

    public void Rotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;
        
        
        // 좌우 -> 회전
        transform.Rotate(0, input.y, 0, Space.Self);

        // 상하 -> 범위 내로 들어오게 해야됨
        _pitch = Mathf.Clamp(_pitch + input.x, _minPitch, _maxPitch);
        //     -> pivot을 회전시켜갸 함
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    public void Move()
    {
        Vector3 input = ReadMoveInput();
        // 새로운 벨로시티값 설정
        Vector3 direction = transform.right * input.x +
                              transform.forward * input.z;

        if (!_isSteamPack)
        {
            Vector3 newVelocity = new Vector3(
                (direction.x) * _moveSpeed,
                _rigidbody.velocity.y,
                (direction.z) * _moveSpeed
            );
            
            // _rigidbody.velocity에 적용
            _rigidbody.velocity = newVelocity;
        }
        else
        {
            Vector3 newVelocity = new Vector3(
                (direction.x) * (_moveSpeed+SteamPack._speedUp),
                _rigidbody.velocity.y,
                (direction.z) * (_moveSpeed+SteamPack._speedUp)
            );
            
            // _rigidbody.velocity에 적용
            _rigidbody.velocity = newVelocity;
        }
    }

    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        
        return new Vector3(-y, x, 0);
    }

    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        return new Vector3(x, 0f, z).normalized;
    }
    
    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Init()
    {
        _isSteamPack = false;
    }
}
