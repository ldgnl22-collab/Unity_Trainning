using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLogger : MonoBehaviour
{
    private void Start()
    {
        BindScoreEvents();
    }

    private void BindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }
    
    private void OnScoreChanged(int score)
    {
        Debug.Log($"ScoreLogger: recorded {score}");
    }
    
    private void UnBindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnDisable()
    {
        UnBindScoreEvents();
    }
}
