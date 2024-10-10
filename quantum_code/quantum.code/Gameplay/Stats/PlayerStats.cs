using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum;

public unsafe partial struct PlayerStats {

    
    public Stat GetRandomStat(Frame frame) {
        QEnumDictionary<StatType,Stat> resolvedStats = frame.ResolveDictionary(Values);
        QDictionaryIterator<StatType,Stat> iterator = resolvedStats.GetEnumerator();
        
        FP value = frame.Global->RngSession.Next();
        Stat result = default;
        while (iterator.MoveNext()) {
            Stat stat = iterator.Current.Value;
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

        return result;
    }


}