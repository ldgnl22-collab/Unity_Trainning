using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [field: SerializeField] public int MAX_HP { get; } = 100;
    [field: SerializeField] public int HP { get; private set; }
    [field: SerializeField] int ATK { get; set; }
    [field: SerializeField] int DEF { get; set; }

    private void Awake() => Init();
    
    private void Init()
    {
        HP = MAX_HP;
    }

    public void SetHp(int value)
    {
        HP = value;
    }
    
    public void Damage(int _damage)
    {
        HP -= _damage;
    }
}
