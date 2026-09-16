using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTag : MonoBehaviour
{
    private static int _tagCount;

    private static int _myTag;

    private void Awake()
    {
        TakeTag();
        SetCubeSize();
    }

    public static int GetTagCount()
    {
        return _tagCount;
    }

    public static void ResetTagCount()
    {
        _tagCount = 0;
        Debug.Log("ResetTagCount: 카운트 리셋");
    }

    private void SetCubeSize()
    {
        int scale = PracticeSettings.DefaultCubeScale;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void TakeTag()
    {
        if (_tagCount >= PracticeSettings.MaxTagCount)
        {
            Debug.Log($"MaxTagCount: 꽉참");
            return;
        }
        
        _tagCount++;
        _myTag = _tagCount;

        Debug.Log($"CubeTag: 내 번호는 {_myTag}이고 지금까지 센 개수는 {_tagCount}입니다");
    }
}
