using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Features.DailyRewards
{
    public class DailyRewards : MonoBehaviour
    {
        [SerializeField] private Tickets _tickets;
        public IReactiveProperty<int> ClaimedRewards { get; } = new ReactiveProperty<int>(0);
        [field: SerializeField] public List<int> Rewards { get; private set; }
        [SerializeField] private float _claimCooldown;

        private DateTime? LastClaimTime
        {
            get
            {
                var date = PlayerPrefs.GetString("LastClaimedTime", null);

                if (!string.IsNullOrEmpty(date))
                    return DateTime.Parse(date);

                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString("LastClaimedTime", value.ToString());
                else
                    PlayerPrefs.DeleteKey("LastClaimedTime");
            }
        }

        private void Awake()
        {
            ClaimedRewards.Value = PlayerPrefs.GetInt("ClaimedRewards", 0);
        }

        public void ClaimReward()
        {
            if (!IsRewardReady())
                return;

            _tickets.AddTickets(Rewards[ClaimedRewards.Value]);

            LastClaimTime = DateTime.UtcNow;

            ClaimedRewards.Value = (ClaimedRewards.Value + 1) % 7;  
        }

        public bool IsRewardReady()
        {
            if (LastClaimTime.HasValue)
            {
                var timeSpan = DateTime.UtcNow - LastClaimTime.Value;

                if (timeSpan.TotalHours > _claimCooldown)
                {
                    LastClaimTime = null;
                    return true;
                }

                if (timeSpan.TotalHours < _claimCooldown)
                {
                    return false;
                }
            }

            return true;
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt("ClaimedRewards", ClaimedRewards.Value);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsRewardReady())
                {
                    Debug.Log("You can claim reward!");
                }
                else
                {
                    var nextClaimTime = LastClaimTime.Value.AddHours(_claimCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    Debug.Log(
                        $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}");
                }
            }
        }
#endif
    }
}