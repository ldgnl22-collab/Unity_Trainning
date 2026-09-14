using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    PlayerMovement _player;

    [SerializeField] private GameObject _grenadePrefab;
    private GameObject _grenadeInstance;
    
    private float _throwForce;
    private float _throwReadyCooldown;
    [field: SerializeField] public float _throwReadyMaxTime { get; private set; } = 1f;
    [field: SerializeField] public float _throwDistance { get; private set; } = 30f;
    [field: SerializeField] public float _grenadeSpeed { get; set; } = 15f;
    [field: SerializeField] public float _explosionRadius  { get; private set; } = 2f;

    [field: SerializeField] public GrenadeEffect _explosionEffect { get; set; }
    [field: SerializeField] public Transform _grenadePos { get; private set; }
    [field: SerializeField] public Transform _cameraPivot { get; private set; }
    
    
    private KeyCode _throwGrenade = KeyCode.Space;
    public bool _isReadyGrenade => Input.GetKey(_throwGrenade);
    public bool _isThrowGrenade => Input.GetKeyUp(_throwGrenade);

    public bool _isTrowed { get; set; } = false;
    
    // 수류탄
    public float _explosionTiming { get; private set; } = 5f;
    public float _explosionCooldawn { get; private set; } = 0f;
    public int _grenadeCount = 3;
    
    private void Awake()
    {
        CacheComponents();
        Init();
    }

    private void Update()
    {
        UseItem();

        if (!_isTrowed) return;
        UpdateCoolTime();
    }
    
    public void UseItem()
    {
        if (_isReadyGrenade)
        {
            if (_throwReadyMaxTime > _throwReadyCooldown)
            {
                _throwReadyCooldown += Time.deltaTime;
                
                Debug.Log($"{_throwReadyCooldown}");
            }
        }

        if (_isThrowGrenade)
        {
            _isTrowed = true;
            Debug.Log("UseItem: 투척");

            _grenadeInstance = Instantiate(_grenadePrefab, _grenadePos.position, Quaternion.identity);

            _grenadeInstance.GetComponent<Rigidbody>().AddForce(
                _cameraPivot.forward *
                (_grenadeSpeed * _throwReadyCooldown), ForceMode.Impulse);

            _throwReadyCooldown = 0f;

            GrenadeBombTimer();
            Destroy(_grenadeInstance, _explosionTiming + 1f);
        }
        _isTrowed = false;
    }
    
    private void GrenadeBombTimer()
    {
        if (_explosionCooldawn > _explosionTiming)
        {
            _explosionCooldawn = 0f;

            Collider[] cols = Physics.OverlapSphere(
                _grenadeInstance.transform.position,
                _explosionRadius);
            
            foreach (Collider col in cols)
            {
                IDamageable damageable;

                damageable = col.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(10);
                    Debug.Log("폭발 피해");
                }
            }

            _explosionCooldawn = 0f;
            _isTrowed = false;
        }
    }

    private void UpdateCoolTime()
    {
        _explosionCooldawn += Time.deltaTime;
    }
    
    private void OnDrawGizmos()
    {
        if (_grenadeInstance != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_grenadeInstance.transform.position, _explosionRadius);
        }
    }

    private void CacheComponents()
    {
    }

    private void Init()
    {
    }
}
