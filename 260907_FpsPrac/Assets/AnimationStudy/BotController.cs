using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotController : MonoBehaviour
{
    public event Action<Vector2> OnMove;

    private Vector2 _prevMovement;
    
    private void Update() => SetMove();

    private void SetMove()
    {
        Vector2 movement = GetMovement();
        if (_prevMovement == movement) return;
        
        OnMove?.Invoke(movement);
        _prevMovement = movement;
        // 이전 프레임의 무브먼트와 같으면 리턴
        // 다르다면 OnMove 실행 + _prevMovement 갱신
    }

    private Vector2 GetMovement()
    {

        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        // 입력 받아서 벡터 반환 GetAxisRaw 사용
        // 단위벡터로 만들면 안됨
    }
}
