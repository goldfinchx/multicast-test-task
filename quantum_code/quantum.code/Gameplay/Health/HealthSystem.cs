namespace Quantum.Gameplay.Health;

public unsafe class HealthSystem : SystemMainThreadFilter<HealthSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Quantum.Health* Health;
        public Transform3D* Transform;
    }

    public override ComponentSet Without => ComponentSet.Create<DeadMarker>();

    // todo move to damage system
    public override void Update(Frame frame, ref Filter filter) {
        if (filter.Health->Value > 0) {
            return;
        }
        
        frame.Add<DeadMarker>(filter.Entity);
        frame.Signals.EntityDeath(filter.Entity, filter.Transform->Position);
    }
}