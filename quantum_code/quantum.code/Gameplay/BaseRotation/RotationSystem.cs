using Photon.Deterministic;

namespace Quantum.Gameplay.BaseRotation;

public unsafe class RotationSystem : SystemMainThreadFilter<RotationSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Rotation* Rotation;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        FP interpolation = filter.Rotation->AdjustedSpeed * frame.DeltaTime;
        filter.Transform->Rotation = FPQuaternion.Slerp(filter.Transform->Rotation, filter.Rotation->Target, interpolation);
    }
}