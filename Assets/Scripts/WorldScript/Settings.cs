using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public string sceneName; // The name of the scene you want to load
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    Resolution[] resolutions;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject pauseMenuUI; // Objek yang mewakili UI menu jeda
    private bool isPaused = false; // Status jeda

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Tombol Escape sebagai tombol jeda
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Mengaktifkan kembali waktu permainan normal
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu permainan
        isPaused = true;
    }

    public void QuitGame()
    {

        Application.Quit();
    }



    void Start()
    {
        // Populate resolution options
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Populate quality options
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        // Set fullscreen mode
        fullscreenToggle.isOn = Screen.fullScreen;

        // Set volume level
        float volume;
        audioMixer.GetFloat("volume", out volume);
        volumeSlider.value = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

}

