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
        
        FPVector3 movement = movementInput.XOY * filter.Movement->Speed;
        filter.Movement->Target = filter.Transform->Position + movement;
    }
    
    private Input GetInput(Frame frame, EntityRef entity) {
        Player* player = frame.Unsafe.GetPointer<Player>(entity);
        return *frame.GetPlayerInput(player->Reference);
    }
    
}