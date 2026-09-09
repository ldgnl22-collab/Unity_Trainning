using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPack : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }

    public const float _speedUp = 10;
    private const int _damage = 10;
    private const float _attackDelay = 0.2f;

    [field: SerializeField] public float _originSpeed { get; private set; }
    [field: SerializeField] public float _originAttackDelay { get; private set; }

    [field: SerializeField] public float _itemCoolTime { get; private set; } = 10f;
    
    [SerializeField] private PlayerWeapon _playerWeapon;
    private Outline _outline;
    
    public void Awake() => CacheComponents();

    private void Start() => Init();
    
    public void Targeting()
    {
        Debug.Log($"스팀팩");
        _outline.enabled = true;
    }

    public void UnTargeting()
    {
        _outline.enabled = false;
    }
    
    public void Interact(IInteractor owner)
    {
        if (!(owner is PlayerController)) return;
        
        PlayerController player = (PlayerController)owner;
        
        PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        _originSpeed = playerMovement._moveSpeed;
        _originAttackDelay = _playerWeapon._shootingSpeed;
        player.GetComponent<PlayerStat>().Damage(_damage);
        
        playerMovement._moveSpeed += _speedUp;
        _playerWeapon._shootingSpeed = _attackDelay;
        
        Debug.Log("Steam Pack 사용");
        playerMovement._isSteamPack = true;
        playerMovement.SteamPackSetInit(_attackDelay);

        Destroy(gameObject);
    }

    private void Init()
    {
        _outline.enabled = false;
    }
    
    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
