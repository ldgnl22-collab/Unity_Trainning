using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, IItem
{
    Rigidbody rb;
    PlayerMovement player;
    
    GameObject _grenadeInstance;

    private Vector3 _throwPos;

    private float _throwForce;
    private float _throwReadyCooldown;
    [field: SerializeField] public float _throwReadyMaxTime { get; private set; } = 1f;
    [field: SerializeField] public float _throwDistance { get; private set; } = 30f;
    [field: SerializeField] public float _grenadeSpeed { get; set; } = 10f;

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
    }
    
    public void UseItem(IUseItem owner)
    {
        if (!(owner is PlayerMovement)) return;
        
        player = (PlayerMovement)owner;
        
        if (player._isReadyGrenade)
        {
            if (_throwReadyMaxTime > _throwReadyCooldown)
            {
                _throwReadyCooldown += Time.deltaTime;
                
                Debug.Log($"{_throwReadyCooldown}");
            }
        }

        if (player._isThrowGrenade)
        {
            Debug.Log("UseItem: 투척");
            
            _grenadeInstance = Instantiate(gameObject, player._grenadePos.position, Quaternion.identity);
            
            _grenadeInstance.GetComponent<Rigidbody>().AddForce(
                 player._cameraPivot.forward * 
                 (_grenadeSpeed * _throwReadyCooldown), ForceMode.Impulse);
             _throwReadyCooldown = 0f;
        }
    }

    private void Explosion()
    {
        
    }

    private void CacheComponents()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
}
