using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    // private void Start()
    // {
    //     BindScoreEvents();
    // }
    //
    // private void BindScoreEvents()
    // {
    //     ScoreManager.Instance.OnScoreChanged += OnScoreChange;
    // }
    //
    // private void UnBindScoreEvents()
    // {
    //     ScoreManager.Instance.OnScoreChanged -= OnScoreChange;
    // }

    private void OnScoreChange(int score)
    {
        Debug.Log($"ScoreTester: OnScoreChange: {score}");
    }

    // private void OnDisable()
    // {
    //     UnBindScoreEvents();
    // }
}
