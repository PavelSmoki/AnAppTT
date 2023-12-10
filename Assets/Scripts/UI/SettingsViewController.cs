using Game.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsViewController : MonoBehaviour
    {
        [SerializeField] private AudioController _audioController;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Toggle _musicToggle;
    
        private void Start()
        {
            var isSoundOn = PlayerPrefs.GetInt("IsSoundsOn", 1) == 1;
            var isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;

            _soundsToggle.isOn = !isSoundOn;
            _musicToggle.isOn = !isMusicOn;
        }

        public void DisableSounds(bool isDisabled)
        {
            _audioController.SetSoundsState(!isDisabled);
        }

        public void DisableMusic(bool isDisabled)
        {
            _audioController.SetMusicState(!isDisabled);
        }
    }
}
