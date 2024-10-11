using System;
using System.Diagnostics;
using Photon.Deterministic;

namespace Quantum.Gameplay.Damage;

public unsafe class DamageSystem : SystemSignalsOnly, ISignalOnAttack {
    
    public void OnAttack(Frame frame, EntityRef victim, EntityRef attacker, FP damage) {
        if (!frame.Unsafe.TryGetPointer(victim, out Quantum.Health* health)) {
            Log.Warn("Damaged entity does not have health component!");
            return;
        }

        health->Value -= damage;
        frame.Signals.OnDamage(victim, attacker, damage);
    }
    
}