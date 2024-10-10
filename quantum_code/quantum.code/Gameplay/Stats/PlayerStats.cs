using System.Linq;
using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum;

public unsafe partial struct PlayerStats {
    
    public Stat GetRandomStat(Frame frame) {
        QList<Stat> resolvedStats = frame.ResolveList(Values);
        QList<Stat>.Iterator iterator = resolvedStats.GetEnumerator();
        
        FP value = frame.Global->RngSession.Next();
        Stat result = Stat.None;
        while (iterator.MoveNext()) {
            Stat stat = iterator.Current;
            if (stat.LimitMaxLevel) {
                if (stat.Level >= stat.MaxLevel) {
                    continue;
                }
            }
            
            if (value > stat.Chance) {
                continue;
            }
            
            if (value >= result.Chance) {
                continue;
            }

            result = stat;
        }
        
        iterator.Dispose();
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