using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Photon.QuantumDemo.Game.Scripts {
    public class LocalInput : MonoBehaviour {

        private PlayerInputActions inputActions;
        private InputAction movementAction;

        private void Awake() {
            inputActions = new PlayerInputActions();
            movementAction = inputActions.Player.Move;
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
            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }
        
    }
}