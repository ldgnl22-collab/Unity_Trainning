using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmClip;
    
    private void Start()
    {
        BgmManager.Instance.PlayBgm(_bgmClip);
    }
}
