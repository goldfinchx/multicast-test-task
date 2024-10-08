namespace Quantum.Gameplay.Health;

public unsafe class HealthSystem : SystemMainThreadFilter<HealthSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Quantum.Health* Health;
        public DeadMarker* DeadMarker;
    }
    
    public override void Update(Frame frame, ref Filter filter) {
        if (filter.DeadMarker->IsDead) {
            return;
        }
        
        if (filter.Health->Value > 0) {
            return;
        }

        filter.DeadMarker->IsDead = true;
    }
}