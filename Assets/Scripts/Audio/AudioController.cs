using UnityEngine;

namespace Game.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundsSource;
        [SerializeField] private AudioSource _musicSource;

        private void Start()
        {
            var isSoundsOn = PlayerPrefs.GetInt("IsSoundsOn", 1) == 1;
            var isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
        
            SetSoundsState(isSoundsOn);
            SetMusicState(isMusicOn);
        }

        public void SetSoundsState(bool isSoundsOn)
        {
            _soundsSource.volume = isSoundsOn ? 0.5f : 0.0f;
            PlayerPrefs.SetInt("IsSoundsOn", isSoundsOn ? 1 : 0);
        }

        public void SetMusicState(bool isMusicOn)
        {
            _musicSource.volume = isMusicOn ? 1f : 0.0f;
            PlayerPrefs.SetInt("IsMusicOn", isMusicOn ? 1 : 0);
        }
    }
}
