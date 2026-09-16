using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGiver : MonoBehaviour
{
    private const int POINT_PER_PRESS = 10;

    private bool _isPressedKey => Input.GetKeyDown(KeyCode.P);

    private void Start()
    {
        ReportPoint();
    }

    private void Update()
    {
        ReadPointKey();
    }

    private void ReportPoint()
    {
        Debug.Log($"PointGiver: 현재점수는 {PracticeScore.Instance.Point}입니다.");
    }

    private void ReadPointKey()
    {
        if (!_isPressedKey) return;
        
        PracticeScore.Instance.AddPoint(POINT_PER_PRESS);
    }
}
