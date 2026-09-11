using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurretController : MonoBehaviour, IDamageable
{
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private LayerMask _targetLayer;
    private int _playerLayer = (1 << 7);

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _coolDown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    
    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyDelay;

    [SerializeField] private Canvas _hpBar;
    
    private float _currentCoolDown;
    private Transform _playerTransform;
    private int _hp = 10;
    
    // private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInTrigger { get { return _playerTransform != null; } }
    private bool _isPlayerInSight;
    private bool _isReadyToFire { get { return _currentCoolDown >= _coolDown; } }
    
    private SphereCollider _sphereCollider;

    public GameObject GameObject { get { return gameObject; } }

    private void Awake()
    {
        Init();
        Debug.Log("<color=red> 컬러로 </color>");
        CacheComponents();
        // _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_targetLayer.Contains(other)) return;
        _hpBar.enabled = true;

        _playerTransform = other.gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_targetLayer.Contains(other)) return;
        _hpBar.enabled = false;
        
        _playerTransform = null;
    }

    private void Update()
    {
        UpdateCurrentCoolDown();
        RayShotToPlayer();
        Rotate();
        Fire();
    }

    private void CacheComponents()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }
    
    

    private void Fire()
    {
        if (!_isPlayerInSight || !_isPlayerInTrigger) return;
        
        Vector3 look = new Vector3(
            _playerTransform.position.x,
            _headTransform.position.y,
            _playerTransform.position.z
            );
        
        _headTransform.LookAt(look);
        _hpBar.transform.forward = (-(_headTransform.forward));

        if (!_isReadyToFire) return;
        
        SpawnBullet();

        _currentCoolDown = 0f;
    }

    private void UpdateCurrentCoolDown()
    {
        if (_isReadyToFire) return;
        
        _currentCoolDown += Time.deltaTime;
    }

    
    
    private void SpawnBullet()
    {
        // 1. 얻어오기
        IPoolable bullet = _bulletPool.Take();
        
        // 2. Transform.position, rotaion 설정
        bullet.tr.position = _muzzlePoint.position;
        bullet.tr.rotation = _muzzlePoint.rotation;
        
        // 3. 활성화
        bullet.tr.gameObject.SetActive(true);
        
        // BulletController bullet = Instantiate(
        //     _bulletPrefab,
        //     _muzzlePoint.position,
        //     _muzzlePoint.rotation
        // );
        
        // 겟 컴포넌트는 비효율적이라 캐스팅방식으로
        (bullet as BulletController).SetData(_bulletDamage, _bulletSpeed, _bulletDestroyDelay);
    }

    private void Rotate()
    {
        if (_isPlayerInSight) return;
        
        _headTransform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void RayShotToPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger) return;

        Vector3 from = new Vector3(
            transform.position.x,
            transform.position.y + _muzzlePoint.position.y,
            transform.position.z
        );
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y + _muzzlePoint.position.y,
            _playerTransform.position.z
        );
        
        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _sphereCollider.radius, _targetLayer))
        {
            // 플레이어 찾았으면 감지 완료 된것임
            if (hit.transform != _playerTransform) return;
            
                _isPlayerInSight = true;
                Debug.Log("플레이어 감지");
        }
    }

    public void TakeDamage(int damage)
    {
        
    }

    private void Init()
    {
        _hpBar.enabled = false;
    }
}
