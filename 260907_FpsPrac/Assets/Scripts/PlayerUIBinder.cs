using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private TempPlayer _Player;
    [SerializeField] private TempPlayerUI _PlayerUI;
    [SerializeField] private ExpGauge _expGauge;
    
    private void Awake() => CacheComponents();

    private void OnEnable() => BindPlayerStatChangeEvents();
    
    private void OnDisable() => UnBindPlayerStatChangeEvents();

    private void BindPlayerStatChangeEvents()
    {
        _Player.OnHealthChange += _PlayerUI.RefreshHealthUI;
        
        _Player.Exp.AddListener(_expGauge.RefreshGauge);
        // 함수를 매개변수로 건내준다.
    }
    
    private void UnBindPlayerStatChangeEvents()
    {
        _Player.OnHealthChange -= _PlayerUI.RefreshHealthUI;
        
        _Player.Exp.RemoveListener(_expGauge.RefreshGauge);
    }
    
    private void CacheComponents()
    {
        _Player = GetComponent<TempPlayer>();
    }
}
