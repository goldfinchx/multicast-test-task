using System;
using Cinemachine;
using Quantum;
using UnityEngine;

[RequireComponent(typeof(EntityView))]
public class PlayerCameraSetup : MonoBehaviour {

    private EntityView entityView;
    
    private void Awake() {
        entityView = GetComponent<EntityView>();
    }

    public void Setup() {
        QuantumGame game = QuantumRunner.Default.Game;
        Frame frame = game.Frames.Verified;

        if (!frame.TryGet(entityView.EntityRef, out Player player)) {
            Debug.LogError("Player component not found on Player entity!");
            return;
        }

        if (!game.PlayerIsLocal(player.Reference)) {
            return;
        }
        
        CinemachineVirtualCamera cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineCamera.Follow = transform;
    }
    
}