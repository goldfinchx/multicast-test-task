using System;

namespace Quantum;

public partial struct Stat : IEquatable<Stat> {
    
    public void Upgrade(int levels = 1) {
        if (LimitLevelFlag && Level >= LimitLevel) {
            return;
        }
        
        Level = LimitLevelFlag ? Math.Min(Level + levels, LimitLevel) : Level + levels;
        Value = DefaultValue + (Level - 1) * UpgradeStep;
    }

    public bool Equals(Stat other) {
        return Chance.Equals(other.Chance) && DefaultValue.Equals(other.DefaultValue) && Level == other.Level && LimitLevelFlag.Equals(other.LimitLevelFlag) && LimitLevel == other.LimitLevel && UpgradeStep.Equals(other.UpgradeStep) && Value.Equals(other.Value);
    }

    public override bool Equals(object obj) {
        return obj is Stat other && Equals(other);
    }
}