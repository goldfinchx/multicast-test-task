using System.Linq;
using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum;

public unsafe partial struct PlayerStats {
    
    public Stat* GetRandomStat(Frame frame) {
        QList<Stat> resolvedStats = frame.ResolveList(Values);
        int value = frame.Global->RngSession.Next(FP._1, FP._100).AsInt;
        Log.Info($"Random value: {value}");
        
        Stat* result = null;
        for (int i = 0; i < resolvedStats.Count; i++) {
            Stat* stat = resolvedStats.GetPointer(i);
            if (stat->LimitLevelFlag) {
                if (stat->Level >= stat->LimitLevel) {
                    continue;
                }
            }
            
            if (result == null) {
                result = stat;
                continue;
            }
            
            if (value > stat->Chance) {
                continue;
            }
            
            if (value >= result->Chance) {
                continue;
            }

            result = stat;
            
        }

        return result;
    }

    public Stat GetAttackDamage(Frame frame) {
        return GetStats(frame).FirstOrDefault(x => x.Type == StatType.AttackDamage);
    }
    
    public Stat GetAttackRange(Frame frame) {
        return GetStats(frame).FirstOrDefault(x => x.Type == StatType.AttackRange);
    }
    
    public Stat GetMovementSpeed(Frame frame) {
        return GetStats(frame).FirstOrDefault(x => x.Type == StatType.MovementSpeed);
    }
    
    private QList<Stat> GetStats(Frame frame) {
        return frame.ResolveList(Values);
    }

}