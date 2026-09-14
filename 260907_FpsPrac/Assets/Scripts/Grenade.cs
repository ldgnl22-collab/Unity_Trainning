using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Grenade : MonoBehaviour, IItem
{
    Rigidbody rb;
    PlayerMovement _player;
    
    GameObject _grenadeInstance;

    private Vector3 _throwPos;

    private float _throwForce;
    private float _throwReadyCooldown;
    [field: SerializeField] public float _throwReadyMaxTime { get; private set; } = 1f;
    [field: SerializeField] public float _throwDistance { get; private set; } = 30f;
    [field: SerializeField] public float _grenadeSpeed { get; set; } = 10f;
    [field: SerializeField] public float _explosionRadius  { get; set; } = 5f;
    
    [SerializeField] private GameObject _explosionEffect;
    
    
    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        Init();
    }
    
    public void UseItem(IUseItem owner)
    {
        if (!(owner is PlayerMovement)) return;
        
        _player = (PlayerMovement)owner;
        
        if (_player._isReadyGrenade)
        {
            if (_throwReadyMaxTime > _throwReadyCooldown)
            {
                _throwReadyCooldown += Time.deltaTime;
                
                Debug.Log($"{_throwReadyCooldown}");
            }
        }

        if (_player._isThrowGrenade)
        {
            Debug.Log("UseItem: 투척");
            
            _grenadeInstance = Instantiate(gameObject, _player._grenadePos.position, Quaternion.identity);
            
            _grenadeInstance.GetComponent<Rigidbody>().AddForce(
                _player._cameraPivot.forward * 
                 (_grenadeSpeed * _throwReadyCooldown), ForceMode.Impulse);
            
             _throwReadyCooldown = 0f;

             if (!_player._isThrowed)
             {
                 _explosionEffect.SetActive(true);
                 Destroy(_grenadeInstance);
                 Debug.Log($"터짐");
             }
        }
    }

    private void Explosion()
    {
        
        Physics.OverlapSphere(transform.position, _explosionRadius);
    }

    private void OnDrawGizmos()
    {
        
    }

    private void CacheComponents()
    {
    }

    private void Init()
    {
        _explosionEffect.SetActive(false);
        // _explosionCooldawn = 0f;
    }
}
