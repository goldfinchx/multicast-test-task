using Quantum.Collections;
using Quantum.Inspector;

namespace Quantum.Gameplay.Stats;

public class StatsSetupSystem : SystemSignalsOnly, ISignalOnComponentAdded<PlayerStats> {
    
    public unsafe void OnAdded(Frame frame, EntityRef entity, PlayerStats* component) {
        if (!frame.Unsafe.TryGetPointer(entity, out Movement* movement)) {
            Log.Error("Movement component not found on Player entity!");
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(entity, out Attacker* attacker)) {
            Log.Error("Attacker component not found on Player entity!");
            return;
        }

        QList<Stat> stats = frame.ResolveList(component->Values);
        for (int i = 0; i < stats.Count; i++) {
            Stat* stat = stats.GetPointer(i);
            stat->Level = 1;
        }

        movement->Speed = component->GetMovementSpeed(frame).DefaultValue;
        attacker->Stats.DamagePerSecond = component->GetAttackDamage(frame).DefaultValue;
        attacker->Stats.Range = component->GetAttackRange(frame).DefaultValue;
    }
}