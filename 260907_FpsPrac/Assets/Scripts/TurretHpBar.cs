using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHpBar : MonoBehaviour
{
    Transform _camera;
    
    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.forward = _camera.forward;
    }
}
