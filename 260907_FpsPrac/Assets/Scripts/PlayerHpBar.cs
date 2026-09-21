using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Image _hpGauge;
    [SerializeField] private TextMeshProUGUI _hpGaugeText;
    
    private PlayerStat _playerStat;
    
    private void Awake() => CacheComponents();
    private void Start() => Init();
    
    public void SetPlayerHpBar(int currentHp, int maxHp)
    {
        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        else if(currentHp <= 0)
        {
            currentHp = 0;
            Debug.Log($"사망");
        }
        _hpGauge.fillAmount = (float)currentHp / maxHp;
        _hpGaugeText.text = $"{currentHp} / {maxHp}";
    }

    private void Init()
    {
        _hpGaugeText.text = $"{_playerStat.HP} / {_playerStat.MAX_HP}";
    }
    
    private void CacheComponents()
    {
        _playerStat = GetComponent<PlayerStat>();
    }
}
