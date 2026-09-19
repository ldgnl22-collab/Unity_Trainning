using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.Dependencies.NCalc;

public class ScoreDisplay : MonoBehaviour
{
    private void OnEnable()
    {
        BindScoreEvents();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Destroy(gameObject);
        }
    }

    private void BindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged.AddListener(OnScoreChanged);
    }
    
    private void UnBindScoreEvents()
    {
        ScoreManager.Instance.OnScoreChanged.RemoveListener(OnScoreChanged);
    }

    public void OnScoreChanged(int score)
    {
        Debug.Log($"ScoreDisplay: score is {score}");
    }

    private void OnDisable()
    {
        UnBindScoreEvents();
    }
}