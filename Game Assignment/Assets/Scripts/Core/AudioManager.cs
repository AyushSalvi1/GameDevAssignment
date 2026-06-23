using UnityEngine;

namespace SlotGame.Core
{
    /// <summary>
    /// Audio manager for handling sound effects and background music.
    /// Provides centralized audio control for the slot machine game.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource backgroundMusicSource;
        [SerializeField] private AudioSource sfxSource;

        [SerializeField] private AudioClip spinSoundClip;
        [SerializeField] private AudioClip winSoundClip;
        [SerializeField] private AudioClip loseSoundClip;
        [SerializeField] private AudioClip backgroundMusicClip;

        [SerializeField] private float masterVolume = 1f;
        [SerializeField] private float sfxVolume = 0.8f;
        [SerializeField] private float musicVolume = 0.5f;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            PlayBackgroundMusic();
        }

        /// <summary>
        /// Play the spin sound effect.
        /// </summary>
        public void PlaySpinSound()
        {
            PlaySFX(spinSoundClip);
        }

        /// <summary>
        /// Play the win sound effect.
        /// </summary>
        public void PlayWinSound()
        {
            PlaySFX(winSoundClip);
        }

        /// <summary>
        /// Play the lose sound effect.
        /// </summary>
        public void PlayLoseSound()
        {
            PlaySFX(loseSoundClip);
        }

        /// <summary>
        /// Play a generic sound effect.
        /// </summary>
        private void PlaySFX(AudioClip clip)
        {
            if (sfxSource != null && clip != null)
            {
                sfxSource.volume = sfxVolume * masterVolume;
                sfxSource.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// Play background music on loop.
        /// </summary>
        private void PlayBackgroundMusic()
        {
            if (backgroundMusicSource != null && backgroundMusicClip != null)
            {
                backgroundMusicSource.clip = backgroundMusicClip;
                backgroundMusicSource.volume = musicVolume * masterVolume;
                backgroundMusicSource.loop = true;
                backgroundMusicSource.Play();
            }
        }

        /// <summary>
        /// Stop background music.
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (backgroundMusicSource != null)
            {
                backgroundMusicSource.Stop();
            }
        }

        /// <summary>
        /// Set master volume level.
        /// </summary>
        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            
            if (backgroundMusicSource != null)
                backgroundMusicSource.volume = musicVolume * masterVolume;
            
            if (sfxSource != null)
                sfxSource.volume = sfxVolume * masterVolume;
        }

        /// <summary>
        /// Toggle audio on/off.
        /// </summary>
        public void ToggleAudio(bool enabled)
        {
            AudioListener.pause = !enabled;
        }
    }
}
