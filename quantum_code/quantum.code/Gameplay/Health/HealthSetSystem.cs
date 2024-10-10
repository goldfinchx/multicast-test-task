namespace Quantum.Gameplay.Health;

public class HealthSetSystem : SystemSignalsOnly, ISignalOnComponentAdded<Quantum.Health> {
    
    public unsafe void OnAdded(Frame frame, EntityRef entity, Quantum.Health* component) {
        component->Value = component->MaxValue;
    }
}