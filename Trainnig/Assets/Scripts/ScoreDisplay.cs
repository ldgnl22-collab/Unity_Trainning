using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreDisplay : MonoBehaviour
{
    private void OnEnable()
    {
        BindScoreEvents();
    }

    private void BindScoreEvents()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        Debug.Log($"ScoreDisplay: score is {score}");
    }
}
