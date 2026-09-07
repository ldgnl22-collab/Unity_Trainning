using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Raycast -> IDamageable
    private Transform _cameraTransform;
    
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _autoFireCooldown;

    private float _coolTime;
    private bool _isPressedFire => Input.GetKey(_fireKey);
    
    private void Awake() => CacheComponents();

    private void Update() => WeaponCoolTime();

    private bool WeaponCoolTime()
    {
        _coolTime += Time.deltaTime;
        
        if (_coolTime < _autoFireCooldown) return false;
        
        return true;
    }

    public void Fire()
    {
        if (!_isPressedFire) return;
        if (!WeaponCoolTime()) return;
        
        IDamageable damageable = GetDamageable();

        if (damageable == null) return;
        
        damageable.TakeDamage(_damage);
        Debug.Log($"Player : {damageable.GameObject.name}에게 발사");
        
        _coolTime = 0f;
    }

    private IDamageable GetDamageable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        IDamageable damageable = null;
        
        if (Physics.Raycast(ray, out hit))
        {
            damageable = hit.transform.GetComponent<IDamageable>();
        }

        return damageable;
    }
    
    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }
}
