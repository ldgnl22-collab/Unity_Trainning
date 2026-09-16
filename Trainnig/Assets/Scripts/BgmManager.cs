using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager Instance { get; private set; }
    private AudioSource _bgmSource;

    private void Awake()
    {
        SetSingleton(Instance);
        _bgmSource = GetComponent<AudioSource>();
    }

    public void PlayBgm(AudioClip bgmClip)
    {
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
    }

    private void SetSingleton(BgmManager instance)
    {
        if (Instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
