using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Raycast -> IDamageable
    private Transform _cameraTransform;
    
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] int maxBulletCount = 30;
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletImpactPrefab;

    [field: SerializeField] public float _shootingSpeed { get; set; } = 0.5f;

    private int currentBulletCount;
    private float _coolTime;
    private bool _hasBullets => currentBulletCount > 0;
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isPressedReload => Input.GetKeyDown(_reloadKey);
    
    // 한 탄알집에 30발 들어갈수 있다고 가정
    // 30 발 다쏘면 총 안쏴짐
    // R 리로드 버튼 눌러야 다시 30발 참
    // 리로드 할수 있는 탄환은 무제한.
    
    private void Awake() => CacheComponents();
    
    private void Start() => Init();

    private void Update()
    {
        WeaponCoolTime();
    }

    private bool WeaponCoolTime()
    {
        _coolTime += Time.deltaTime;
        
        if (_coolTime >= _shootingSpeed)
        {
            return true;
        }
        return false;
    }

    public void Fire()
    {
        // if (_isPressedReload)
        // {
        //     ReLoad();
        // }
        
        if (!_isPressedFire) return;
        if (!WeaponCoolTime()) return;
        if (currentBulletCount <= 0)
        {
            Debug.Log("탄약이 부족합니다.");
            return;
        }
        
        currentBulletCount--;
        _coolTime = 0f;
        PlayEffect();

        if (!TryGetDamageable(out IDamageable damageable)) return;
        damageable.TakeDamage(_damage);
        
        Debug.Log($"Player : {damageable.GameObject.name}에게 발사");
        currentBulletCount--;
        
        Debug.Log($"탄약 {currentBulletCount} 남음");
    }

    private void PlayEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void PlayBulletImpactEffect(RaycastHit hit)
    {
        Transform effectTransform = Instantiate(_bulletImpactPrefab).transform;
        effectTransform.position = hit.point;
        effectTransform.forward = hit.normal; // normal은 부딪힌 면의 수직방향
    }

    // private void ReLoad()
    // {
    //     if (currentBulletCount >= 0 && currentBulletCount <= maxBulletCount)
    //     {
    //         currentBulletCount = maxBulletCount;
    //         Debug.Log("탄약이 장전됨");
    //     }
    //     else if (currentBulletCount >= maxBulletCount)
    //     {
    //         currentBulletCount = maxBulletCount;
    //         Debug.Log("탄약이 가득참");
    //     }
    //     else
    //     {
    //         return;
    //     }
    // }

    public void Reload()
    {
        if (!_isPressedReload) return;
        
        currentBulletCount = maxBulletCount;
    }

    private bool TryGetDamageable(out IDamageable damageable)
    {
        bool result = false;
        damageable = null;
        
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _range))
        {
            PlayBulletImpactEffect(hit);
            result = hit.transform.TryGetComponent(out damageable);
        }

        return result;
    }
    
    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {
        _coolTime = 0f;
        currentBulletCount = maxBulletCount;
    }
}
