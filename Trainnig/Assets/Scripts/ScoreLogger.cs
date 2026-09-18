using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLogger : MonoBehaviour
{
    private void OnEnable()
    {
        BindScoreEvents();
    }

    private void BindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged.AddListener(OnScoreChanged);
    }
    
    public void OnScoreChanged()
    {
        Debug.Log($"ScoreLogger: recorded ");
    }
    
    private void UnBindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged.RemoveListener(OnScoreChanged);
    }

    private void OnDisable()
    {
        UnBindScoreEvents();
    }
}
