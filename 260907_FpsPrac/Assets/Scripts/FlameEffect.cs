using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    // 설정한 딜레이 후 자동으로 꺼지게 하기
    // gameObject.SetActive(false)

    [SerializeField] private float _flameDelay;
    [SerializeField] private bool _isDestroy;
    [SerializeField] private bool _playInStart;
    private float _timer;

    private void OnEnable() => ResetTimer();
    private void Start() => gameObject.SetActive(_playInStart);
    
    private void Update()
    {
        FlameDelayTime();
        EndEffect();
    }
    
    public void Play()
    {
        // 딜레이 초기화
        ResetTimer();
    }

    private void ResetTimer()
    {
        _timer = 0f;
    }

    private void FlameDelayTime()
    {
        _timer += Time.deltaTime;
    }

    private void EndEffect()
    {
        if (_timer < _flameDelay) return;
        
        if(_isDestroy) Destroy(this.gameObject);
        else gameObject.SetActive(false);
    }
}
