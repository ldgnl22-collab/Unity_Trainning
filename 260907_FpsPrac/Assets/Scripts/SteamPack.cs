using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPack : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }

    public const float _speedUp = 10;
    private const int _damage = 10;
    private const float _attackDelay = -0.2f;

    private const float _itemCoolTime = 20f;

    private float _coolTime;

    private Outline _outline;
    
    public void Awake() => CacheComponents();

    private void Start() => Init();

    private void Update() => UpCountTimer();
    
    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void UnTargeting()
    {
        _outline.enabled = false;
    }

    public void UpCountTimer()
    {
        _coolTime = Time.time + _itemCoolTime;
    }
    
    public void Interact(IInteractor owner)
    {
        if (!(owner is PlayerController)) return;
        
        PlayerController player = (PlayerController)owner;
        
        Debug.Log("Steam Pack");
        PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        
        playerMovement._isSteamPack = true;
        playerMovement.SteamPackSetCoolTime(_attackDelay);
        
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
