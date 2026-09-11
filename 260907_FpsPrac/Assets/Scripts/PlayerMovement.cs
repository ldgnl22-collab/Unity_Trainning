using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUseItem
{
    public const float ORIGIN_MOVE_SPEED = 5f;
    public const float ORIGIN_ATK_SPEED = 0.5f;
    [field: SerializeField] public float _moveSpeed { get; set; } = 5f;
    [field: SerializeField] public float _jumpForce { get; set; } = 5f;

    [field: SerializeField] public Transform _cameraPivot { get; private set; }
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private SteamPack _steamPack;
    [SerializeField] private Grenade _grenade;
    [field: SerializeField] public Transform _grenadePos { get; set; }
    
    [SerializeField] LayerMask _groundMask;
    
    // private KeyCode _jump = KeyCode.Space;
    // private bool _isJump => Input.GetKeyDown(_jump);
    // private bool _isPossibleJump;
    
    private KeyCode _throwGrenade = KeyCode.Space;
    public bool _isReadyGrenade => Input.GetKey(_throwGrenade);
    public bool _isThrowGrenade => Input.GetKeyUp(_throwGrenade);

    private float _checkGroundRange;
    private float _pitch;
    private Rigidbody _rigidbody;
    
    // 스팀팩
    public bool _isSteamPack;
    private float _durationCool;
    private float _duration;
    private bool _isTimer;

    private float _grenadeDistanceTimer;

    private void Awake() => CacheComponents();

    private void Start() => Init();

    private void Update()
    {
        EndSteamPack();
        UseGrenade();
    }

    private void EndSteamPack()
    {
        if (_durationCool > _duration)
        {
            _durationCool = 0f;
            _isSteamPack = false;
            _moveSpeed = ORIGIN_MOVE_SPEED;
            _weapon._shootingSpeed = ORIGIN_ATK_SPEED;
            Debug.Log("스팀팩 종료");
            
            return;
        }

        if (_isSteamPack)
        {
            _durationCool += Time.deltaTime;
            Debug.Log($"{_durationCool} 초 동안 지속중");
        }
    }
    
    public void UseGrenade()
    {
        _grenade.UseItem(this);
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

        Vector3 newVelocity = new Vector3(
            (direction.x) * _moveSpeed,
            _rigidbody.velocity.y,
            (direction.z) * _moveSpeed
        );
        
        // _rigidbody.velocity에 적용
        _rigidbody.velocity = newVelocity;
    }

    // public void Jump()
    // {
    //     if (_isJump && _isPossibleJump)
    //     {
    //         _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    //     }
    // }
    //
    // public void PossibleJump()
    // {
    //     Ray ray =  new Ray(transform.position, Vector3.down);
    //     RaycastHit hit;
    //
    //     if (Physics.Raycast(ray, out hit, _checkGroundRange))
    //     {
    //         if (!_groundMask.Contains(hit.collider.gameObject.layer))
    //         {
    //             _isPossibleJump = false;
    //             return;
    //         }
    //
    //         Vector3 newVelocity = new Vector3(1f, 0f, 1f) * _moveSpeed;
    //         
    //         _rigidbody.velocity = newVelocity;
    //         
    //         _isPossibleJump = true;
    //     }
    // }

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

    public void AddSpeed(float speed, float shootingSpeed, float duration)
    {
        _isSteamPack = true;
        _duration = duration;
        _moveSpeed += speed;
        _weapon._shootingSpeed = shootingSpeed;
    }
}
