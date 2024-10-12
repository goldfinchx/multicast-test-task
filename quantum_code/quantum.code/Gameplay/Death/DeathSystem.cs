using Photon.Deterministic;

namespace Quantum.Gameplay.Death;

public class DeathSystem : SystemSignalsOnly, ISignalOnDamage {
    public void OnDamage(Frame frame, EntityRef victim, EntityRef attacker, FP damage) {
        if (!frame.TryGet(victim, out Quantum.Health health)) {
            Log.Warn("Damaged entity does not have health component!");
            return;
        }
        
        if (health.Value > 0) {
            return;
        }

        frame.Signals.OnDeath(victim, attacker);
        frame.Destroy(victim);
    }
}