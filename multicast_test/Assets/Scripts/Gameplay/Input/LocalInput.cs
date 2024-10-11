using System.Numerics;
using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Gameplay.Input {
    public class LocalInput : MonoBehaviour {

        private PlayerInputActions inputActions;
        private InputAction movementAction;
        private InputAction rotationAction;

        private void Awake() {
            inputActions = new PlayerInputActions();
            movementAction = inputActions.Player.Move;
            rotationAction = inputActions.Player.Rotation;
        }

        private void OnEnable() {
            inputActions.Enable();
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        private void OnDisable() {
            inputActions.Disable();
        }

        private void PollInput(CallbackPollInput callback) {
            Quantum.Input input = new();
            input.Movement = movementAction.ReadValue<Vector2>().ToFPVector2();
            input.Rotation = rotationAction.ReadValue<Vector2>().ToFPVector2();
            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }
        
    }
}