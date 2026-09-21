using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Image _hpGauge;
    [SerializeField] private TextMeshProUGUI _hpGaugeText;

    private void Awake()
    {
        
    }
    
    public void SetPlayerHpBar(int currentHp, int maxHp)
    {
        int hp = currentHp / maxHp;
        _hpGauge.fillAmount = hp * 100;
        
        _hpGaugeText.text = $"{hp}/{maxHp}";
    }

    private void Init()
    {
    }
}
