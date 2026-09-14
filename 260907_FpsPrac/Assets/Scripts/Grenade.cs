using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Grenade : MonoBehaviour, IItem
{
    Rigidbody rb;
    PlayerMovement _player;
    
    [field: SerializeField] public GameObject _grenadeInstance { get; private set; }
    
    private float _throwForce;
    private float _throwReadyCooldown;
    [field: SerializeField] public float _throwReadyMaxTime { get; private set; } = 1f;
    [field: SerializeField] public float _throwDistance { get; private set; } = 30f;
    [field: SerializeField] public float _grenadeSpeed { get; set; } = 15f;
    [field: SerializeField] public float _explosionRadius  { get; private set; } = 5f;

    [field: SerializeField] public GrenadeEffect _explosionEffect { get; set; }

    public bool _isTrowed { get; set; } = false;
    
    private void Awake()
    {
        CacheComponents();
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
            _isTrowed = true;
            Debug.Log("UseItem: 투척");
            
            _grenadeInstance = Instantiate(gameObject, _player._grenadePos.position, Quaternion.identity);
            
            _grenadeInstance.GetComponent<Rigidbody>().AddForce(
                _player._cameraPivot.forward * 
                 (_grenadeSpeed * _throwReadyCooldown), ForceMode.Impulse);
            
            _explosionEffect.transform.position = _grenadeInstance.transform.position;
            _explosionEffect.transform.rotation = _grenadeInstance.transform.rotation;
            
             _throwReadyCooldown = 0f;
             Destroy(_grenadeInstance, _player._explosionTiming);
        }
    }

    private void CacheComponents()
    {
    }

    private void Init()
    {
    }
}
