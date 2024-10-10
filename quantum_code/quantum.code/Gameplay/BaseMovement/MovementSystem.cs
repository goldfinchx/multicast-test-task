using Photon.Deterministic;

namespace Quantum.Gameplay.BaseMovement;

public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Movement* Movement;
        public Transform3D* Transform;
    }
    
    public override void Update(Frame frame, ref Filter filter) {
        FP interpolation = frame.DeltaTime * filter.Movement->AdjustedSpeed;
        filter.Transform->Position = FPVector3.Lerp(filter.Transform->Position, filter.Movement->Target, interpolation);
    }
    
}