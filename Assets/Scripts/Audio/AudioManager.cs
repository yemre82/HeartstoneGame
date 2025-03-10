using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Music Settings")]
        public AudioSource musicSource;
        public AudioClip menuMusic;
        public AudioClip gameMusic;

        [Header("Sound Effects")]
        public AudioSource sfxSource;
        public AudioClip cardDrawSound;
        public AudioClip cardPlaySound;
        public AudioClip hoverSound;
        public AudioClip endTurnSound;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayMenuMusic();
        }

        public void PlayMenuMusic()
        {
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayGameMusic()
        {
            musicSource.clip = gameMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (sfxSource.volume > 0)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}
