using Main.Controllers;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private Button playButton;
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private Button quitButton;

        [Header("Settings")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private TMP_Text sfxVolumeText;
        [SerializeField] private Slider bgmVolumeSlider;
        [SerializeField] private TMP_Text bgmVolumeText;

        private void Awake()
        {
            playButton.onClick.AddListener(OnClickPlay);
            settingsButton.onClick.AddListener(OnClickSettings);
            backToMenuButton.onClick.AddListener(OnClickBackToMenu);
            quitButton.onClick.AddListener(OnClickQuit);
            sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeSliderValueChanged);
            bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeSliderValueChanged);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnClickPlay);
            settingsButton.onClick.RemoveListener(OnClickSettings);
            backToMenuButton.onClick.RemoveListener(OnClickBackToMenu);
            quitButton.onClick.RemoveListener(OnClickQuit);
            sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeSliderValueChanged);
            bgmVolumeSlider.onValueChanged.RemoveListener(OnBgmVolumeSliderValueChanged);
        }

        private void Start()
        {
            OpenMainMenu();
            if (AudioController.Instance == null)
            {
                return;
            }

            AudioController.Instance.PlayMenuBGM();
            SetupVolumeSlider(sfxVolumeSlider, 1, 0, AudioController.Instance.GetVolumeSfx());
            SetupVolumeSlider(bgmVolumeSlider, 1, 0, AudioController.Instance.GetVolumeBgm());
        }

        private void OnClickPlay()
        {
            OnClick();
            SceneManager.LoadSceneAsync(1);
        }

        private void OnClickSettings()
        {
            OnClick();
            OpenSettings();
        }

        private void OnClickBackToMenu()
        {
            OnClick();
            OpenMainMenu();
        }

        private void OnClickQuit()
        {
            OnClick();
            Application.Quit();
        }

        private void OnSfxVolumeSliderValueChanged(float value)
        {
            AudioController.Instance.SetVolumeSfx(value);
            sfxVolumeText.text = $"SFX: {Utils.GetFormatVolumeValue(value)}";
        }

        private void OnBgmVolumeSliderValueChanged(float value)
        {
            AudioController.Instance.SetVolumeBgm(value);
            bgmVolumeText.text = $"BGM: {Utils.GetFormatVolumeValue(value)}";
        }

        public void OnClick()
        {
            AudioController.Instance?.PlayClickSfx();
        }

        private void OpenMainMenu()
        {
            mainMenuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        private void OpenSettings()
        {
            settingsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }

        private void SetupVolumeSlider(Slider slider, float maxValue, float minValue, float value)
        {
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            slider.value = value;
        }
    }
}