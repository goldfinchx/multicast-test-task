using System.Collections;
using System.Collections.Generic;
using Quantum;
using UnityEngine;

[RequireComponent(typeof(EntityView), typeof(MeshRenderer))]
public class EnemyDamageAnimator : MonoBehaviour {

    private EntityView entityView;
    private MeshRenderer meshRenderer;

    private void Awake() {
        entityView = GetComponent<EntityView>();
        meshRenderer = GetComponent<MeshRenderer>();
        QuantumEvent.Subscribe<EventDamage>(listener: this, handler: HandleDamageEvent);
    }

    private void HandleDamageEvent(EventDamage @event) {
        if (@event.Victim != entityView.EntityRef) {
            return;
        }
        
        StartCoroutine(ShowDamageEffect());
    }
    
    private IEnumerator ShowDamageEffect() {
        Color defaultColor = meshRenderer.material.color;
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.material.color = defaultColor;
    }
    
}