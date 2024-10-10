namespace Quantum.Gameplay.Events;

public class EventsSystem : SystemSignalsOnly, ISignalOnStatUpdate {
    
    public void OnStatUpdate(Frame frame, EntityRef playerEntity, Stat stat) {
        frame.Events.StatUpdate(stat);
    }
}