using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeBoard : MonoBehaviour
{
    [SerializeField] private Slider _volumSlider;
    [SerializeField] private TextMeshProUGUI _volumeText;

    private void Start()
    {
        BindSliderEvents();
        UpdateText(_volumSlider.value);
    }

    private void BindSliderEvents()
    {
        _volumSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        UpdateText(volume);
    }

    private void UpdateText(float volume)
    {
        _volumeText.text = $"Volume: {volume}";
    }
}
