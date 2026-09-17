using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurretHpBar : MonoBehaviour
{
    private Transform _camera;

    private Image _hpBarImage;
    
    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Start()
    {
        BindOnHpChange();
    }

    private void BindOnHpChange()
    {
        
    }

    private void UnBindOnHpChange(int hp)
    {
        
    }

    private void Update()
    {
        transform.forward = _camera.forward;
    }

    private void OnHpChange(int hp)
    {
        
    }
}
