using Photon.Deterministic;

namespace Quantum.Gameplay.PlayerRotation;

public unsafe class PlayerRotationSystem : SystemMainThreadFilter<PlayerRotationSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Player* Player;
        public Rotation* Rotation;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        Input* input = frame.GetPlayerInput(filter.Player->Reference);
        FPVector3 rotationInput = new(0, input->Rotation.X, 0);
        FPQuaternion rotationDelta = FPQuaternion.Euler(rotationInput);
        FPQuaternion target = filter.Rotation->Target * rotationDelta;
        filter.Rotation->Target = target;
    }
}