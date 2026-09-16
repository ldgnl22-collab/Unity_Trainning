using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BoltShooter : MonoBehaviour
{
    private GameObject _bolt;
    private bool _isFireKey => Input.GetKeyDown(KeyCode.Space);
    
    
    
    private void Update()
    {
        PoolFire();
    }

    private void PoolFire()
    {
        if(!_isFireKey) return;

        _bolt = BoltPool.Instance.Take();

        if (_bolt == null) return;
        else _bolt.GetComponent<Bolt>().ResetState(transform);
    }

    private void ResetBolt()
    {
        
    }
}
