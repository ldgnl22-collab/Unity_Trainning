using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
        
    }
    
    [SerializeField] private IntEvent _onScoreChanged;
    
    private int _score;

    public static ScoreManager Instance;

    public IntEvent OnScoreChanged => _onScoreChanged;
    
    // public event Action<int> OnScoreChanged;

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(10);
        }
    }

    public void AddScore(int amount)
    {
        _score += amount;
        
        _onScoreChanged.Invoke(_score);
    }

    private void SetSingleton()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }*/
        
        Instance = this;
    }
}
