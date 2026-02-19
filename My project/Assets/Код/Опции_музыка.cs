using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Опции_музыка : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Text volumeText; // Добавляем ссылку на текст

    private const string VolumePrefKey = "MusicVolume";
    private const float DefaultVolume = 0.75f;

    void Awake()
    {
        var existing = FindObjectOfType<Опции_музыка>();
        if (existing != null && existing != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, DefaultVolume);

        // Обновляем текст ДО установки значения слайдера
        UpdateTextDisplay(savedVolume);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        if (audioMixer == null) return;

        volume = Mathf.Clamp01(volume);
        float dbVolume = volume > 0 ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("volume", dbVolume);

        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();

        UpdateTextDisplay(volume); // Обновляем текст при изменении громкости
    }

    private void UpdateTextDisplay(float volume)
    {
        if (volumeText != null)
        {
            // Отображаем в процентах (0–100 %)
            volumeText.text = $"{Mathf.Round(volume * 100)}%";
        }
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }
}
