using System;
using Cinemachine;
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
        private CinemachineVirtualCamera virtualCamera;
        
        public EntityView AttachedEntity { get; private set; }
        private IDisposable subscription;

        private void Awake() {
            service = FindObjectOfType<ViewService>();
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            slider = GetComponentInChildren<Scrollbar>();
            healthText = GetComponentInChildren<TextMeshProUGUI>();
        }


        private void Update() {
            if (AttachedEntity is null) {
                return;
            }

            transform.position = AttachedEntity.transform.position + offset;
            transform.rotation = Quaternion.LookRotation(transform.position - virtualCamera.transform.position);
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
            if (AttachedEntity is null) {
               
                return;
            }

            QuantumGame game = QuantumRunner.Default.Game;
            Frame frame = game.Frames.Verified;

            if (!frame.TryGet(AttachedEntity.EntityRef, out Health health)) {
                return;
            }

            slider.size = health.Value.AsFloat / health.MaxValue;
            healthText.text = $"{health.Value.AsInt} / {health.MaxValue}";
        }
    }
}