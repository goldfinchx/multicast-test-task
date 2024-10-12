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
                .Where(evt => service.IsLocalPlayer(evt.Killer))
                .Subscribe(_ => UpdateText());
        }

        private void OnDisable() {
            subscription?.Dispose();
        }
        
        private void UpdateText() {
            int killCount = text.text == "" ? 0 : int.Parse(text.text);
            text.text = (killCount + 1).ToString();
        }

    }
}
