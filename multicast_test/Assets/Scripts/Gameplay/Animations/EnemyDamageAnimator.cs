using System.Collections;
using Quantum;
using UnityEngine;

namespace Gameplay.Animations {
    [RequireComponent(typeof(EntityView), typeof(MeshRenderer))]
    public class EnemyDamageAnimator : MonoBehaviour {

        [SerializeField] private float damageEffectDuration = 0.1f;
    
        private EntityView entityView;
        private MeshRenderer meshRenderer;
        private Color defaultColor;

        private void Awake() {
            entityView = GetComponent<EntityView>();
            meshRenderer = GetComponent<MeshRenderer>();
            defaultColor = meshRenderer.material.color;
            QuantumEvent.Subscribe<EventDamage>(listener: this, handler: HandleDamageEvent);
        }

        private void HandleDamageEvent(EventDamage @event) {
            if (@event.Victim != entityView.EntityRef) {
                return;
            }
        
            StartCoroutine(ShowDamageEffect());
        }
    
        private IEnumerator ShowDamageEffect() {
            meshRenderer.material.color = defaultColor * Color.black;
            yield return new WaitForSeconds(damageEffectDuration);
            meshRenderer.material.color = defaultColor;
        }
    
    }
}