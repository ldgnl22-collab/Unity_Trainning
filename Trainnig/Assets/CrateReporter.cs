using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateReporter : MonoBehaviour
{
    private const string TAG_GROUND = "Ground";
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TAG_GROUND))
        {
            Debug.Log("CrateReporter: 바닥에 닿았습니다.");
            
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
