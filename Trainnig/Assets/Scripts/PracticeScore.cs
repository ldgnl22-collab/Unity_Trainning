using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeScore : MonoBehaviour
{
    private int _point;
    
    public static PracticeScore Instance { get; private set; }

    public int Point => _point;

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        ResetPoint();
    }

    public void AddPoint(int value)
    {
        _point += value;
        Debug.Log($"PracticeScore: 점수가 {_point}이 되었습니다.");
    }

    public void ResetPoint()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _point = 0;
            Debug.Log($"PracticeScore: 점수가 {_point}로 초기화");
        }
    }

    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
