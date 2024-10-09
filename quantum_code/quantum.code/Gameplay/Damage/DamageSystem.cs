using System;
using System.Diagnostics;
using Photon.Deterministic;

namespace Quantum.Gameplay.Damage;

public unsafe class DamageSystem : SystemSignalsOnly, ISignalOnDamage {
    
    public void OnDamage(Frame frame, EntityRef victim, EntityRef attacker, int damage) {
        if (!frame.TryGet(victim, out Quantum.Health health)) {
            Log.Warn("Damaged entity does not have health component!");
            return;
        }
        
        health.Value -= damage;
        frame.Set(victim, health);
    }
    
}