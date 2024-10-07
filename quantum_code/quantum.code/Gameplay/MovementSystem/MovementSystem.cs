using System.Diagnostics;
using Photon.Deterministic;

namespace Quantum.Gameplay.MovementSystem;

public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Movement* Movement;
        public Transform3D* Transform;
    }
    
    public override void Update(Frame frame, ref Filter filter) {
        FPVector3 newPosition = CalculateNewPosition(filter.Transform->Position, filter.Movement->Target, filter.Movement->Speed);
        filter.Transform->Position = newPosition;
    }

    private FPVector3 CalculateNewPosition(FPVector3 transformPosition, FPVector3 movementTarget, FP movementSpeed) {
        return FPVector3.Lerp(transformPosition, movementTarget, movementSpeed);
    }
    
}