using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    public string mainParameterName = "Main Volume";
    public string sfxParameterName = "SFX Volume";
    public string musicParameterName = "Music Volume";

    private const float MinDB = -80f;
    private const float MaxDB = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat(mainParameterName, 100f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxParameterName, 100f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicParameterName, 100f);

        SetVolume(mainVolumeSlider.value, mainParameterName);
        SetVolume(sfxVolumeSlider.value, sfxParameterName);
        SetVolume(musicVolumeSlider.value, musicParameterName);

        mainVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, mainParameterName));
        sfxVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, sfxParameterName));
        musicVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, musicParameterName));
    }

    public void SetVolume(float sliderValue, string parameterName)
    {
        float dB = Mathf.Lerp(MinDB, MaxDB, sliderValue / 100f);

        audioMixer.SetFloat(parameterName, dB);

        PlayerPrefs.SetFloat(parameterName, sliderValue);
        PlayerPrefs.Save();

        Debug.Log($"Salvato {parameterName} = {sliderValue}");
    }

    private float GetVolumeFromMixer(string parameterName)
    {
        if (audioMixer.GetFloat(parameterName, out float dB))
        {
            float normalized = Mathf.Pow(10f, dB / 20f);

            return Mathf.RoundToInt(normalized * 100f);
        }
        else
        {
            Debug.LogWarning($"Parametro {parameterName} non trovato nel mixer!");
            return 100f; // default
        }
    }
}
