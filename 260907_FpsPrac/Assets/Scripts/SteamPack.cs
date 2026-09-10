using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPack : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }
    public const float _speedUp = 10;
    private const int _damage = 10;
    private const float _attackDelay = 0.2f;
    
    [field: SerializeField] public float _itemCoolTime { get; private set; } = 10f;
    
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

        player.AddMoveSpeed(_speedUp, _attackDelay, _itemCoolTime);
        
        // 함수화 해야함
        player.GetComponent<PlayerStat>().Damage(_damage);
        
        Debug.Log("Steam Pack 사용");

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
