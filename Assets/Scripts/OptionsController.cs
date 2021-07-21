using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    [Header("Options Config.")]
    [SerializeField] GameObject optionsPanel;

    [Header("Audio Sources")]
    [SerializeField] internal AudioSource musicSource;
    [SerializeField] internal AudioSource fxSource;
    [SerializeField] internal AudioSource shotSource;

    [Header("Audio Sliders")]
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider fxVolumeSlider;

    [Header("Audio Sources UI")]
    [SerializeField] Text musicVolumeTitleText;
    [SerializeField] Text musicVolumeText;
    [SerializeField] Text fxVolumeText;

    [Header("Audio Clips")]
    [SerializeField] internal AudioClip startClip;

    // PlayerPrefs constants
    string _firstPlaytrough = "FirstPlaythrough";
    string _musicVolume = "MusicVolume";
    string _fxVolume = "FXVolume";

    // Start is called before the first frame update
    void Start()
    {
        initializePlayerPrefs();
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            toggleOptions();
        }
    }

    void toggleOptions()
    {
        if (optionsPanel.activeInHierarchy) { musicSource.Play(); }
        else { musicSource.Pause(); }

        Time.timeScale = optionsPanel.activeInHierarchy ? 1 : 0;
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }

    void initializePlayerPrefs()
    {
        if (PlayerPrefs.GetInt(_firstPlaytrough) == 0)
        {
            PlayerPrefs.SetFloat(_musicVolume, 1f);
            PlayerPrefs.SetFloat(_fxVolume, 0.8f);
            PlayerPrefs.SetInt(_firstPlaytrough, 1);
        }

        float musicVolume = PlayerPrefs.GetFloat(_musicVolume);
        float fxVolume = PlayerPrefs.GetFloat(_fxVolume);

        musicSource.volume = musicVolume;
        fxSource.volume = fxVolume;

        musicVolumeSlider.value = musicVolume;
        fxVolumeSlider.value = fxVolume;

        musicVolumeText.text = Mathf.Round(musicVolume * 100).ToString();
        fxVolumeText.text = Mathf.Round(fxVolume * 100).ToString();
    }

    public IEnumerator changeMusic(AudioClip clip)
    {
        float currentVolume = musicSource.volume;

        for (float v = currentVolume; v > 0; v -= 0.02f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();

        for (float v = 0; v < currentVolume; v += 0.02f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }
    }

    internal void playFXSound(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }

    internal void playShotSound(AudioClip clip)
    {
        shotSource.PlayOneShot(clip);
    }

    public void changeMusicVolume()
    {
        float volume = musicVolumeSlider.value;

        musicSource.volume = volume;
        musicVolumeText.text = Mathf.Round(volume * 100).ToString();

        PlayerPrefs.SetFloat(_musicVolume, volume);
    }

    public void changeFXVolume()
    {
        float volume = fxVolumeSlider.value;

        fxSource.volume = volume;
        fxVolumeText.text = Mathf.Round(volume * 100).ToString();

        PlayerPrefs.SetFloat(_fxVolume, volume);
    }

    public void toggleMuteMusicVolume()
    {
        musicSource.mute = !musicSource.mute;
        musicVolumeTitleText.text = $"Music Volume {(musicSource.mute ? "(Muted!)" : "")}";
    }

    public void quitGame() => Application.Quit();
}
