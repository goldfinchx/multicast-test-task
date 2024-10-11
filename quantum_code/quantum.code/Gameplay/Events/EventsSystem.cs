using Photon.Deterministic;

namespace Quantum.Gameplay.Events;

public class EventsSystem : SystemSignalsOnly, ISignalOnStatUpdate, ISignalOnDamage, ISignalOnDeath, ISignalOnEnemySpawn {
    
    public void OnStatUpdate(Frame frame, EntityRef playerEntity, Stat stat) {
        frame.Events.StatUpdate(stat);
    }

    public void OnDamage(Frame frame, EntityRef victim, EntityRef attacker, FP damage) {
        frame.Events.Damage(victim, attacker, damage);
    }

    public void OnDeath(Frame frame, EntityRef entity, EntityRef killer) {
        frame.Events.EnemyDeath(entity, killer);
    }

    public void OnEnemySpawn(Frame frame, EntityRef enemy) {
        frame.Events.EnemySpawn(enemy);
    }
}