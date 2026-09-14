using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private TextMeshProUGUI _hpBarText;

    public void SetPlayerHpBar(int currentHp, int maxHp)
    {
        currentHp *= 100;
        _hpBar.fillAmount = currentHp / maxHp;
    }
}
