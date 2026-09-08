using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }
    private Outline _outline;
    
    public void Awake() => CacheComponents();

    private void Start() => Init();
    
    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void UnTargeting()
    {
        _outline.enabled = false;
    }
    
    public void Interact(IInteractor owner)
    {
        // owner의 능력치 상승..
        // 인벤토리고 들어가거나
        // 무기 생성
        // 장탄

        // if (!(owner is PlayerController)) return;
        //
        // PlayerController player = (PlayerController)owner;
        
        // 이동속도 변화
        
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
