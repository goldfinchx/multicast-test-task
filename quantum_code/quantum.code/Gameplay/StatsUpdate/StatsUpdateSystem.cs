using System;
using System.Linq;
using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum.Gameplay.StatsUpdate;

public unsafe class StatsUpdateSystem : SystemSignalsOnly, ISignalOnStatUpgrade, ISignalOnStatSetupCommand {

    public void OnStatUpgrade(Frame frame, EntityRef playerEntity, Stat stat) {
        UpdateStat(frame, playerEntity, stat);
    }
    
    private void UpdateStat(Frame frame, EntityRef playerEntity, Stat stat) {
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
                attacker->Stats.DamagePerSecond = statValue;
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
        
        frame.Signals.OnStatUpdate(playerEntity, stat);
    }


    public void OnStatSetupCommand(Frame frame, int player, int typeIndex) {
        if (!frame.TryGetPlayerEntity(player, out EntityRef playerEntity)) {
            Log.Error("Player entity not found!");
            return;
        }

        if (!frame.Unsafe.TryGetPointer(playerEntity, out PlayerStats* statsComponent)) {
            Log.Error("PlayerStats component not found on Player entity!");
            return;
        }

        QList<Stat> stats = frame.ResolveList(statsComponent->Values);
        StatType statType = (StatType) typeIndex;
        Stat stat = stats.FirstOrDefault(x => x.Type == statType);
        UpdateStat(frame, playerEntity, stat);
    }
}