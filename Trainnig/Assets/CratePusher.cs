using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePusher : MonoBehaviour
{
    [SerializeField] private float _power = 10f;

    private Rigidbody _body;
    private Vector3 _inputDirection;
    
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        ApplyInput();
    }

    private void ReadInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _inputDirection = new Vector3(horizontal, 0f, vertical);
    }

    private void ApplyInput()
    {
        _body.AddForce(_inputDirection * _power);
        // _body.velocity = _inputDirection * _power;
    }
}
