using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagReporter : MonoBehaviour
{
    private bool _isButtonDown => Input.GetKeyDown(KeyCode.R);
    
    private void Start()
    {
        ReportCount();
    }

    private void Update()
    {
        ReturnCountButton();
    }

    private void ReturnCountButton()
    {
        if (!_isButtonDown) return;
        
        CubeTag.ResetTagCount();
    }

    private void ReportCount()
    {
        Debug.Log($"TagReporter: {CubeTag.GetTagCount()}");
    }
}
