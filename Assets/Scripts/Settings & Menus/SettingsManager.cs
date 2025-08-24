using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioSettingsController audioController;

    public void SaveSettingsAndBack()
    {
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);

        Debug.Log("Impostazioni salvate e tornato al menu principale");
    }

    public void ResetSettings()
    {
        PlayerPrefs.SetFloat(audioController.mainParameterName, 100f);
        PlayerPrefs.SetFloat(audioController.musicParameterName, 100f);
        PlayerPrefs.SetFloat(audioController.sfxParameterName, 100f);
        PlayerPrefs.Save();

        if (audioController != null)
        {
            audioController.mainVolumeSlider.value = 100f;
            audioController.musicVolumeSlider.value = 100f;
            audioController.sfxVolumeSlider.value = 100f;

            audioController.SetVolume(100f, audioController.mainParameterName);
            audioController.SetVolume(100f, audioController.musicParameterName);
            audioController.SetVolume(100f, audioController.sfxParameterName);
        }

        Debug.Log("Impostazioni ripristinate ai valori di default");
    }
}
