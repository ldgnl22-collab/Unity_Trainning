using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPack : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }

    public const float _speedUp = 10;
    private const int _damage = 10;
    private const float _attackDelay = -0.2f;

    public const float _itemCoolTime = 20f;

    private float _coolTime;

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

    public void UpCountTimer()
    {
        _coolTime = Time.time + _itemCoolTime;
        Debug.Log($"{_coolTime} 초 동안 지속중");
    }
    
    public void Interact(IInteractor owner)
    {
        if (!(owner is PlayerController)) return;
        
        PlayerController player = (PlayerController)owner;
        
        PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        
        Debug.Log("Steam Pack 사용");
        playerMovement._isSteamPack = true;
        playerMovement.SteamPackSetCoolTime(_attackDelay);
        UpCountTimer();

        if (_coolTime > _itemCoolTime)
        {
            _coolTime = 0f;
            playerMovement._isSteamPack = false;
            Debug.Log("스팀팩 종료");
            return;
        }
        Destroy(gameObject);
    }

    private void Init()
    {
        _coolTime = 0f;
        _outline.enabled = false;
    }
    
    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
