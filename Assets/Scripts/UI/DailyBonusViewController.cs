using System.Collections.Generic;
using Game.Features.DailyRewards;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class DailyBonusController : MonoBehaviour
    {
        private const float BarStep = 0.1428f;

        [SerializeField] private Image _giftsBar;
        [SerializeField] private TextMeshProUGUI _redeemedGiftsLabel;
        [SerializeField] private GameObject _infoSegment;
        [SerializeField] private GameObject _giftSegment;
        [SerializeField] private List<TextMeshProUGUI> _dayLabels;
        [SerializeField] private List<TextMeshProUGUI> _giftAmountLabels;
        [SerializeField] private TextMeshProUGUI _giftSegmentDayLabel;
        [SerializeField] private TextMeshProUGUI _giftSegmentGiftAmountLabel;
        [SerializeField] private DailyRewards _dailyRewards;

        private void Start()
        {
            _dailyRewards.ClaimedRewards.Subscribe(_ => UpdateInfo());
            for (int i = 0; i < _dailyRewards.Rewards.Count - 1; i++)
            {
                _dayLabels[i].text = $"DAY{i + 1}";
                _giftAmountLabels[i].text = $"X{_dailyRewards.Rewards[i]}";
            }
        }

        private void UpdateInfo()
        {
            _giftsBar.rectTransform.localScale = new Vector3(BarStep * _dailyRewards.ClaimedRewards.Value, 1, 0);
            _redeemedGiftsLabel.text = $"{_dailyRewards.ClaimedRewards.Value}/7";
        }

        private void OnEnable()
        {
            UpdateInfo();

            _infoSegment.SetActive(true);
            _giftSegment.SetActive(false);
        }

        public void OnClaimSectionPressed()
        {
            if (_dailyRewards.IsRewardReady())
            {
                var currentDay = _dailyRewards.ClaimedRewards.Value + 1;
                _giftSegmentDayLabel.text = $"DAY {currentDay.ToString()}";
                _giftSegmentGiftAmountLabel.text =
                    _dailyRewards.Rewards[_dailyRewards.ClaimedRewards.Value].ToString();

                _dailyRewards.ClaimReward();
                
                _infoSegment.SetActive(false);
                _giftSegment.SetActive(true);
            }
        }
    }
}