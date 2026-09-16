using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _testText;
    [SerializeField] private AudioClip _audioButtonClip;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TMP_Dropdown _dropdown;
    
    private AudioSource _audioSource;

    private int _score;

    private void Start()
    {
        Init();
        UpdateText();
    }

    private void Update()
    {
        ToggleTextUpdate();
        DropdownTextUpdate();
    }

    public void AddScore()
    {
        _score += 10;
        UpdateText();
    }

    public void PlayClip()
    {
        _audioSource.clip = _audioButtonClip;
        _audioSource.Play();
    }

    public void ToggleTextUpdate()
    {
        if (_toggle.isOn) _testText.text = "True";
        else _testText.text = "False";
    }

    public void DropdownTextUpdate()
    {
        if (_dropdown.value == 0) _testText.text = "None";
        else if (_dropdown.value == 1) _testText.text = "First";
        else if (_dropdown.value == 2) _testText.text = "Second";
    }

    private void UpdateText()
    {
        _scoreText.text = $"Score: {_score}";
    }

    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
