using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [SerializeField] private float _meterPerSecond = 8f;
    [SerializeField] private float _lifeSecond = 2f;

    private float _elapsed;

    private void Update()
    {
        MoveForward();
        CountLifeTime();
    }

    public void ResetState(Transform startPos)
    {
        transform.position = startPos.position;
        _elapsed = 0f;
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _meterPerSecond * Time.deltaTime);
    }

    private void CountLifeTime()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed >= _lifeSecond)
        {
            BoltPool.Instance.Return(gameObject);
        }
    }
}
