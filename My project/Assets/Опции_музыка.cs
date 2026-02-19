using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Опции_музыка : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        // LOAD the saved volume (default 0.75f if nothing saved)
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        // SET the slider to the saved value
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        // Apply the volume to the mixer
        SetVolume(savedVolume);

        // Add listener for when slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer not assigned!");
            return;
        }

        // Convert 0-1 range to decibels
        float dbVolume = volume > 0 ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("volume", dbVolume);

        // Save the volume setting
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}