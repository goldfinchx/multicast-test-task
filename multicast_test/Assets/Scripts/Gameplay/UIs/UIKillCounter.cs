using System;
using Quantum;
using TMPro;
using UniRx;
using UnityEngine;

namespace Gameplay.UIs {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIKillCounter : MonoBehaviour {
        
        [SerializeField] private ViewSimulationMediator service;
        private TextMeshProUGUI text;
        private IDisposable subscription;
        
        private void Awake() {
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
            
            Debug.Log("Enemy killed!");

            if (!frame.TryGet(@event.Killer, out Player player)) {
                return;
            }
            
            Debug.Log("Player found!");
            
            if (!frame.TryGet(@event.Killer, out Statistics statistics)) {
                Debug.LogError("Statistics component not found on Player entity!");
                return;
            }
            
            Debug.Log("Statistics found!");

            if (!game.PlayerIsLocal(player.Reference)) {
                return;
            }
            
            Debug.Log("Player is local!");  
            
            UpdateText(statistics.Kills);
        }

        private void UpdateText(int killCount) {
            text.text = killCount.ToString();
            Debug.Log("Kill count updated!");
        }

    }
}
