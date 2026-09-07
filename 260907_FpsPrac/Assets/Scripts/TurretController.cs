using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private const string TAG_PLAYER = "Player";

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _coolDown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    
    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyDelay;
    
    private float _currentCoolDown;
    private Transform _playerTransform;
    
    // private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInTrigger { get { return _playerTransform != null; } }
    private bool _isPlayerInSight;
    private bool _isReadyToFire { get { return _currentCoolDown >= _coolDown; } }
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        CacheComponents();
        // _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_PLAYER))
        {
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_PLAYER))
        {
            _playerTransform = null;
        }
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
        BulletController bullet = Instantiate(
            _bulletPrefab,
            _muzzlePoint.position,
            _muzzlePoint.rotation
        );
        
        bullet.SetData(_bulletDamage, _bulletSpeed, _bulletDestroyDelay);
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
            transform.position.y + _muzzlePoint.position.y / 2,
            transform.position.z
        );
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y + _muzzlePoint.position.y / 2,
            _playerTransform.position.z
        );
        
        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _sphereCollider.radius))
        {
            // 플레이어 찾았으면 감지 완료 된것임
            if (hit.transform.CompareTag(TAG_PLAYER))
            {
                _isPlayerInSight = true;
                Debug.Log("플레이어 감지");
            }
        }
    }
}
