using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource AudioSource;
    private float musicVolume;
    public Slider volumeSlider; // Ссылка на UI‑слайдер

    void Start()
    {
        // Загружаем сохранённую громкость или устанавливаем по умолчанию
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Настраиваем слайдер, если он подключён
        if (volumeSlider != null)
        {
            volumeSlider.value = musicVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }

        AudioSource.Play();
        UpdateVolume(musicVolume); // Применяем громкость
    }

    void OnVolumeSliderChanged(float newVolume)
    {
        UpdateVolume(newVolume);
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume); // Ограничиваем диапазон 0–1
        AudioSource.volume = musicVolume;

        // Сохраняем в PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
