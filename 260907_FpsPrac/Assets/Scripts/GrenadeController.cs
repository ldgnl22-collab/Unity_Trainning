using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    PlayerMovement _player;

    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private GameObject _effectPrefab;
    private GameObject _grenadeInstance;
    private GameObject _effectInstance;
    
    private float _throwForce;
    private float _throwReadyCooldown;
    
    [field: SerializeField] public float _throwReadyMaxTime { get; private set; } = 1f;
    [field: SerializeField] public float _throwDistance { get; private set; } = 30f;
    [field: SerializeField] public float _grenadeSpeed { get; set; } = 15f;
    [field: SerializeField] public float _explosionRadius  { get; private set; } = 2f;
    [field: SerializeField] public Transform _grenadePos { get; private set; }
    [field: SerializeField] public Transform _cameraPivot { get; private set; }
    
    
    private KeyCode _throwGrenade = KeyCode.Space;
    public bool _isReadyGrenade => Input.GetKey(_throwGrenade);
    public bool _isThrowGrenade => Input.GetKeyUp(_throwGrenade);
    public bool _isTrowed { get; set; } = false;
    
    // 수류탄
    private int _grenadeCount = 3;
    private float _explosionTiming = 5f;
    private float _explosionCooldown = 0f;
    private WaitForSeconds _wait;
    private Coroutine[] _interactRoutine;
    private int _useCount;
    
    private void Awake()
    {
        CacheComponents();
        Init();
    }

    private void Update()
    {
        UseItem();
        // UpdateCoolTime();
    }

    private bool CheckCanUseGrenade()
    {
        if (_useCount >= _grenadeCount)
        {
            // 수류탄 없음
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void UseItem()
    {
        if (!CheckCanUseGrenade()) return;
        
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

            // _grenadeInstance.GetComponent<Rigidbody>().AddForce(
            //     _cameraPivot.forward *
            //     (_grenadeSpeed * _throwReadyCooldown), ForceMode.Impulse);

            _grenadeInstance.GetComponent<Rigidbody>().velocity = _cameraPivot.forward *
                                                                  (_grenadeSpeed * _throwReadyCooldown);

            _throwReadyCooldown = 0f;

            GrenadeBombTimer();
        }
    }
    
    private void StartRoutine()
    {
        if (_useCount >= _grenadeCount)
        {
            _useCount = 0;
        }
        if (_interactRoutine[_useCount] != null) return;

        _interactRoutine[_useCount] = StartCoroutine(GrenadeBombTimerRoutine(_grenadeInstance));
    }

    private void StopRoutine()
    {
        if (_useCount >= _grenadeCount)
        {
            StopCoroutine(GrenadeBombTimerRoutine(_grenadeInstance));
            _interactRoutine[_grenadeCount-1] = null;
            return;
        }
        if (_interactRoutine[_useCount] == null) return;

        StopCoroutine(GrenadeBombTimerRoutine(_grenadeInstance));
        _interactRoutine[_useCount] = null;
    }
    
    private void GrenadeBombTimer()
    {
        StartRoutine();

        _useCount++;
        // if(!_isTrowed)
        // StopRoutine();
    }
    
    private IEnumerator GrenadeBombTimerRoutine(GameObject grenadeInstance)
    {
        yield return _wait;

        Collider[] cols = Physics.OverlapSphere(
            grenadeInstance.transform.position,
            _explosionRadius);
        
        _effectInstance = Instantiate(_effectPrefab, grenadeInstance.transform.position, Quaternion.identity);
        _effectInstance.GetComponent<ParticleSystem>().Play();
        
        foreach (Collider col in cols)
        {
            IDamageable damageable;

            damageable = col.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(10);
                Debug.Log("폭발 피해");
            }
            
            _isTrowed = false;
            Destroy(grenadeInstance, 2f);
        }
    }

    private void OnDestroy()
    {
        StopRoutine();
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
        _wait = new WaitForSeconds(_explosionTiming);
        _interactRoutine = new Coroutine[_grenadeCount];
        _isTrowed = false;
    }
}
