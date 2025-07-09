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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainVolumeSlider.value = GetVolumeFromMixer(mainParameterName);
        sfxVolumeSlider.value = GetVolumeFromMixer(sfxParameterName);
        musicVolumeSlider.value = GetVolumeFromMixer(musicParameterName);

        mainVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, mainParameterName));
        sfxVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, sfxParameterName));
        musicVolumeSlider.onValueChanged.AddListener(value => SetVolume(value, musicParameterName));
    }

    public void SetVolume(float volume, string parameterName)
    {

        Debug.Log("Setting" + parameterName + "to" + volume);
        if (volume < 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20);

    }

    private float GetVolumeFromMixer(string parameterName)
    {
        float currentVolume;
        audioMixer.GetFloat(parameterName, out currentVolume);
        Debug.Log("Current" + parameterName + ":" + currentVolume);
        return Mathf.Pow(10, currentVolume / 20);
    }

}
