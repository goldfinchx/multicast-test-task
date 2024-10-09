namespace Quantum.Gameplay.Death;

public class DeathSystem : SystemSignalsOnly, ISignalOnDamage {
    public void OnDamage(Frame frame, EntityRef victim, EntityRef attacker, int damage) {
        if (!frame.TryGet(victim, out Health health)) {
            Log.Warn("Damaged entity does not have health component!");
            return;
        }
        
        if (health.Value > 0) {
            return;
        }

        frame.Destroy(victim);
        frame.Signals.OnDeath(victim, attacker);
    }
}