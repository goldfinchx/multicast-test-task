using System;
using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum.Gameplay.StatsSync;

public unsafe class StatsSyncWithComponentsSystem : SystemSignalsOnly, ISignalOnPlayerSpawn, ISignalOnStatUpgrade {

    public void OnPlayerSpawn(Frame frame, EntityRef player) {
        SyncAllStats(frame, player);
    }
    
    public void OnStatUpgrade(Frame frame, EntityRef playerEntity, Stat stat) {
        SyncStat(frame, playerEntity, stat);
    }
    
    private void SyncAllStats(Frame frame, EntityRef playerEntity) {
        if (!frame.Unsafe.TryGetPointer(playerEntity, out PlayerStats* statsComponent)) {
            Log.Error("PlayerStats component not found on Player entity!");
            return;
        }

        QList<Stat> stats = frame.ResolveList(statsComponent->Values);
        foreach (Stat stat in stats) {
            SyncStat(frame, playerEntity, stat);
        }
    }

    private void SyncStat(Frame frame, EntityRef playerEntity, Stat stat) {
        if (!frame.Unsafe.TryGetPointer(playerEntity, out Movement* movement)) {
            Log.Error("Movement component not found on Player entity!");
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(playerEntity, out Attacker* attacker)) {
            Log.Error("Attacker component not found on Player entity!");
            return;
        }
        
        FP statValue = stat.Level == 1 ? stat.DefaultValue : stat.Value;
        switch (stat.Type) {
            case StatType.AttackDamage:
                attacker->Stats.Damage = statValue.AsInt;
                break;
            case StatType.AttackRange:
                attacker->Stats.Range = statValue;
                break;
            case StatType.MovementSpeed:
                movement->Speed = statValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
    
}