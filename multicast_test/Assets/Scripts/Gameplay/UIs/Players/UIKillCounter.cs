using System;
using Quantum;
using TMPro;
using UniRx;
using UnityEngine;

namespace Gameplay.UIs.Players {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIKillCounter : MonoBehaviour {
        
        private ViewService service;
        private TextMeshProUGUI text;
        private IDisposable subscription;
        
        private void Awake() {
            service = FindObjectOfType<ViewService>();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable() {
            subscription = service.EventsSubject
                .OfType<object, EventEnemyDeath>()
                .Subscribe(HandleEnemyDeath);
        }

        private void OnDisable() {
            subscription?.Dispose();
        }
        
        private void HandleEnemyDeath(EventEnemyDeath @event) {
            QuantumGame game = QuantumRunner.Default.Game;
            Frame frame = game.Frames.Verified;
            
            if (!frame.TryGet(@event.Killer, out Player player)) {
                return;
            }
            
            if (!frame.TryGet(@event.Killer, out Statistics statistics)) {
                Debug.LogError("Statistics component not found on Player entity!");
                return;
            }
            
            if (!game.PlayerIsLocal(player.Reference)) {
                return;
            }

            UpdateText(statistics.Kills);
        }

        private void UpdateText(int killCount) {
            text.text = killCount.ToString();
        }

    }
}
