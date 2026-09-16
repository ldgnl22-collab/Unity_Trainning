using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainShooter : MonoBehaviour
{
    [SerializeField] private GameObject _boltPrefab;
    
    private bool _isFireKey => Input.GetKeyDown(KeyCode.Space);

    private void Update()
    {
        ReadFireKey();
    }

    private void ReadFireKey()
    {
        if (!_isFireKey) return;
        
        Instantiate(_boltPrefab, transform.position, transform.rotation);
    }
}
