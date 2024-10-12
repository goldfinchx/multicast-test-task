using System;
using System.Text;
using Quantum;
using Quantum.Gameplay.StatsCommands;
using TMPro;
using UniRx;
using UnityEngine;

namespace Gameplay.UIs.Players {
    public class UIStat : MonoBehaviour {
    
        [SerializeField] private StatType type;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private TextMeshProUGUI value;
        [SerializeField] private TextMeshProUGUI chance;

        private ViewService service;
        private IDisposable subscription;

        private void Awake() {
            service = FindObjectOfType<ViewService>();
        }

        private void OnEnable() {
            subscription = service.EventsSubject
                .OfType<object, EventStatUpdate>()
                .Where(evt => evt.Stat.Type == type && service.IsLocalPlayer(evt.Player))
                .Subscribe(HandleStatUpdate);
            Invoke(nameof(NotifySetup), 0.1f);
        }
        
        private void NotifySetup() {
            QuantumRunner.Default.Game.SendCommand(new StatSetupCommand {
                StatType = (int) type
            });
        }
        
        private void OnDisable() {
            subscription?.Dispose();
        }

        private void HandleStatUpdate(EventStatUpdate evt) {
            UpdateStat(evt.Stat);
        }
        
        private void UpdateStat(Stat stat) {
            title.text = GetStatTitle(stat.Type);
            StringBuilder stringBuilder = new();
            
            stringBuilder
                .Append("Level:")
                .Append(stat.Level);
            level.text = stringBuilder.ToString();
            
            stringBuilder.Clear()
                .Append("Value:")
                .Append(stat.Value);
            value.text = stringBuilder.ToString();

            stringBuilder.Clear()
                .Append("Chance:")
                .Append(stat.Chance).Append("%");
            chance.text = stringBuilder.ToString();
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
