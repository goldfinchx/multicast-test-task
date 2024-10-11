using Photon.Deterministic;

namespace Quantum.Gameplay.PlayerMovement;

public unsafe class PlayerMovementSystem : SystemMainThreadFilter<PlayerMovementSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Player* Player;
        public Movement* Movement;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        Input input = GetInput(frame, filter.Entity);
        FPVector2 movementInput = input.Movement;
        if (movementInput == default) {
            return;
        }
        
        FPVector3 forward = filter.Transform->Forward;
        FPVector3 right = filter.Transform->Right;
        forward.Y = 0;
        right.Y = 0;
        
        FPVector3 rotatedMovement = movementInput.X * right + movementInput.Y * forward;
        
        filter.Movement->Target = filter.Transform->Position + rotatedMovement;
    }
    
    private Input GetInput(Frame frame, EntityRef entity) {
        Player* player = frame.Unsafe.GetPointer<Player>(entity);
        return *frame.GetPlayerInput(player->Reference);
    }
    
}