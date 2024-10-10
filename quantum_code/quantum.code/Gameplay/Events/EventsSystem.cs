namespace Quantum.Gameplay.Events;

public class EventsSystem : SystemSignalsOnly, ISignalOnStatUpdate, ISignalOnDamage {
    
    public void OnStatUpdate(Frame frame, EntityRef playerEntity, Stat stat) {
        frame.Events.StatUpdate(stat);
    }

    public void OnDamage(Frame frame, EntityRef victim, EntityRef attacker, int damage) {
        frame.Events.Damage(victim, attacker, damage);
    }
}