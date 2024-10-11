using System;
using Quantum;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay.UIs.Enemies {
    public class UIHealthBar : MonoBehaviour {
        [SerializeField] private Scrollbar slider;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Vector3 offset;

        private ViewService service;
        
        public EntityView AttachedEntity { get; private set; }
        private IDisposable subscription;

        private void Awake() {
            service = FindObjectOfType<ViewService>();
            slider = GetComponentInChildren<Scrollbar>();
            healthText = GetComponentInChildren<TextMeshProUGUI>();
        }


        private void Update() {
            if (AttachedEntity is null) {
                return;
            }

            Debug.Log(AttachedEntity.transform.position);
            transform.position = AttachedEntity.transform.position + offset;
        }

        private void OnEnable() {
            subscription = service.EventsSubject
                .OfType<object, EventDamage>()
                .Where((damageEvent, _) => damageEvent.Victim == AttachedEntity.EntityRef)
                .Subscribe(HandleDamageEvent);
        }

        private void OnDisable() {
            subscription?.Dispose();
            AttachedEntity = null;
        }

        private void HandleDamageEvent(EventDamage @event) {
            if (@event.Victim != AttachedEntity.EntityRef) {
                return;
            }

            UpdateVisuals();
        }

        public void Setup(EntityView entityView) {
            AttachedEntity = entityView;
            gameObject.SetActive(true);
            UpdateVisuals();
        }

        private void UpdateVisuals() {
            if (AttachedEntity == null) {
                return;
            }

            QuantumGame game = QuantumRunner.Default.Game;
            Frame frame = game.Frames.Verified;

            if (!frame.TryGet(AttachedEntity.EntityRef, out Health health)) {
                Debug.LogError("Health component not found on entity with health bar!");
                return;
            }

            slider.size = health.Value.AsFloat / health.MaxValue;
            healthText.text = $"{health.Value.AsInt} / {health.MaxValue}";
        }
    }
}