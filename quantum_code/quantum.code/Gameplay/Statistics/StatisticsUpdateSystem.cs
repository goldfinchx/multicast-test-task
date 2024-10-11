namespace Quantum.Gameplay.Statistics;

public unsafe class StatisticsUpdateSystem : SystemSignalsOnly, ISignalOnDeath {
    
    public void OnDeath(Frame frame, EntityRef entity, EntityRef killer) {
        if (!frame.Unsafe.TryGetPointer(killer, out Quantum.Statistics* statistics)) {
            return;
        }

        statistics->Kills++;
    }
}