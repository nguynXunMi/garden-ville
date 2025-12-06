using System;
using UnityEngine;

namespace Main.Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;

        [Header("SFX")]
        [SerializeField] private AudioClip sowSeedAudioClip;
        [SerializeField] private AudioClip[] clickAudioClips;

        [Header("BGM")]
        [SerializeField] private AudioClip mainBGMAudioClip;
        [SerializeField] private AudioClip menuBGMAudioClip;

        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
    
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void PlaySFX(AudioClip clip)
        {
            sfxAudioSource.PlayOneShot(clip);
        }

        private void PlayBGM(AudioClip clip)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.Play();
            bgmAudioSource.loop = true;
        }

        public void SetVolumeSfx(float value) => sfxAudioSource.volume = value;
        public void SetVolumeBgm(float value) => bgmAudioSource.volume = value;
        public float GetVolumeSfx() => sfxAudioSource.volume;
        public float GetVolumeBgm() => bgmAudioSource.volume;

        public void PlayMenuBGM() => PlayBGM(menuBGMAudioClip);
        public void PlayMainBGM() => PlayBGM(mainBGMAudioClip);
        public void PlaySowSeedSfx() => PlaySFX(sowSeedAudioClip);
        public void PlayClickSfx() => PlaySFX(clickAudioClips[new System.Random().Next(clickAudioClips.Length)]);
    }
}
