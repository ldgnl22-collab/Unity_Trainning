using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int _score;

    public static ScoreManager Instance;

    public Action<int> ScoreChanged;

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
        
        // ScoreChanged?.Invoke(_score);

        if (ScoreChanged != null)
        {
            ScoreChanged(_score);
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
    }
}
