using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SignalLight : MonoBehaviour
{
    private Renderer _renderer;
    private readonly WaitForSeconds _waitOneSeconds = new WaitForSeconds(1f);

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    
    private void Start()
    {
        StartCoroutine(RunSignalRoutine());
    }
    
    private IEnumerator RunSignalRoutine()
    {
        while (true)
        {
            _renderer.material.color = Color.red;
            Debug.Log("빨간불");
            yield return new WaitForSeconds(1f);
            
            _renderer.material.color = Color.yellow;
            Debug.Log("노란불");

            yield return new WaitForSeconds(1f);
            
            _renderer.material.color = Color.green;
            Debug.Log("초록불");

            yield return new WaitForSeconds(1f);
        }
    }
}
