using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _hitClip;
    
    void Update()
    {
        ReadSoundKeys();
    }

    private void ReadSoundKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _source.PlayOneShot(_hitClip); // 1번 재생한 것은 끊기지 않고 끝까지 재생된다.
            // _source.Play(); // source에 있는 clip을 재생하고 다시 재생할 때 이전건 끊는다
            Debug.Log("SfxTrigger: 효과음을 냈습니다.");
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            _source.Stop();
        }
    }
}
