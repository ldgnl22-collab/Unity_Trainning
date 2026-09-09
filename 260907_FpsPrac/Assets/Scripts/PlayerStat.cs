using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [field: SerializeField] int HP { get; set; }
    [field: SerializeField] int ATK { get; set; }
    [field: SerializeField] int DEF { get; set; }

    public void Damage(int _damage)
    {
        HP -= _damage;
    }
}
