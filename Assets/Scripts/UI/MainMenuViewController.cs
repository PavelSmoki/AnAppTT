using Game.Features;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Canvas _dailyBonusView;
        [SerializeField] private Canvas _settingsView;
        [SerializeField] private TextMeshProUGUI _ticketsLabel;
        [SerializeField] private Tickets _tickets;

        private Canvas _currentActiveView;

        private void Start()
        {
            UpdateInfo();
            _tickets.PlayersTickets.Subscribe(_ => UpdateInfo());
        }

        private void UpdateInfo()
        {
            _ticketsLabel.text = _tickets.PlayersTickets.Value.ToString();
        }

        public void ShowDailyBonusView()
        {
            _dailyBonusView.gameObject.SetActive(true);
            SetActiveView(_dailyBonusView);
        }

        public void ShowSettingsView()
        {
            _settingsView.gameObject.SetActive(true);
            SetActiveView(_settingsView);
        }

        private void SetActiveView(Canvas view)
        {
            _currentActiveView = view;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_currentActiveView != null)
                {
                    CloseView();
                }
            }
        }

        private void CloseView()
        {
            _currentActiveView.gameObject.SetActive(false);
        }
    }
}