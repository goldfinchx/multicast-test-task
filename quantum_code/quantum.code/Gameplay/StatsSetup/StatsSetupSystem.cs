using Quantum.Collections;

namespace Quantum.Gameplay.StatsSetup;

public unsafe class StatsSetupSystem : SystemSignalsOnly, ISignalOnPlayerSpawn {

    public void OnPlayerSpawn(Frame frame, EntityRef player) {
        if (!frame.Unsafe.TryGetPointer(player, out PlayerStats* statsComponent)) {
            Log.Error("Player component not found on Player entity!");
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(player, out Movement* movement)) {
            Log.Error("Movement component not found on Player entity!");
            return;
        }

        if (!frame.Unsafe.TryGetPointer(player, out Attacker* attacker)) {
            Log.Error("Attacker component not found on Player entity!");
            return;
        }
        
        HeroStatsConfig defaultHeroStats = frame.FindAsset<HeroStatsConfig>(frame.RuntimeConfig.DefaultHeroStats);
        QList<Stat> stats = defaultHeroStats.GetStats(frame);
        statsComponent->Values = stats;
        
        movement->Speed = defaultHeroStats.MovementSpeed.DefaultValue;
        attacker->Stats.DamagePerSecond = defaultHeroStats.AttackDamage.DefaultValue;
        attacker->Stats.Range = defaultHeroStats.AttackRange.DefaultValue;
    }
}