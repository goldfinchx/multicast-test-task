using System;
using Quantum;
using TMPro;
using UniRx;
using UnityEngine;

namespace Gameplay.UIs {
    public class UIStat : MonoBehaviour {
    
        [SerializeField] private ViewSimulationMediator mediator;
        [SerializeField] private StatType type;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private TextMeshProUGUI value;
        [SerializeField] private TextMeshProUGUI chance;
        
        private IDisposable subscription;

        private void OnEnable() {
            subscription = mediator.EventsSubject
                .OfType<object, EventStatUpdate>()
                .Where(evt => evt.Stat.Type == type)
                .Subscribe(evt => UpdateStat(evt.Stat));
        }
        
        private void OnDisable() {
            subscription?.Dispose();
        }

        private void UpdateStat(Stat stat) {
            title.text = GetStatTitle(stat.Type);
            level.text = stat.Level.ToString();
            value.text = stat.Value.ToString();
            chance.text = stat.Chance + "%";
        }

        private string GetStatTitle(StatType statType) {
            return statType switch {
                StatType.AttackDamage => "Damage",
                StatType.AttackRange => "Range",
                StatType.MovementSpeed => "Speed",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
    }
}
