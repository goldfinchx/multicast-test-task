using System.IO;
using Photon.Deterministic;

namespace Quantum.Gameplay.BaseRotation;

public unsafe class RotationSystem : SystemMainThreadFilter<RotationSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Rotation* Rotation;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        FP deltaTime = frame.DeltaTime;
        filter.Transform->Rotation = FPQuaternion.Lerp(filter.Transform->Rotation, filter.Rotation->Target, deltaTime * filter.Rotation->Speed);
    }
}