using UniRx;
using UnityEngine;

namespace Game.Features
{
    public class Tickets : MonoBehaviour
    {
        public IReactiveProperty<int> PlayersTickets { get; } = new ReactiveProperty<int>(0);

        private void Awake()
        {
            PlayersTickets.Value = PlayerPrefs.GetInt("PlayersTickets", 10);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt("PlayersTickets", PlayersTickets.Value);
        }

        public void AddTickets(int tickets)
        {
            PlayersTickets.Value += tickets;
        }
    }
}
