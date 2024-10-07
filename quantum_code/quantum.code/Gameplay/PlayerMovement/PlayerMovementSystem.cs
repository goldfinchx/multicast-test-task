using System.Runtime.CompilerServices;
using Photon.Deterministic;

namespace Quantum.Gameplay.PlayerMovement;

public unsafe class PlayerMovementSystem : SystemMainThreadFilter<PlayerMovementSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public PlayerMarker* PlayerMarker;
        public Movement* Movement;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        Input* playerInput = frame.GetPlayerInput(0);
        if (playerInput == null) {
            return;
        }

        FPVector2 movementInput = playerInput->Movement;
        if (movementInput == default) {
            return;
        }
        
        FPVector3 newPosition = CalculateNewPosition(filter.Transform->Position, movementInput, filter.Movement->Speed);
        filter.Movement->Target = newPosition;
    }

    private FPVector3 CalculateNewPosition(FPVector3 currentPosition, FPVector2 movementInput, FP speed) {
        return currentPosition + movementInput.XOY * speed;
    }
}