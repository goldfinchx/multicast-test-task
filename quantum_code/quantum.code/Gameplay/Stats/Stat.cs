using System;

namespace Quantum;

public partial struct Stat : IEquatable<Stat> {

    public static Stat None = new() { Chance = 1 };

    public void Upgrade(int levels = 1) {
        if (LimitMaxLevel && Level >= MaxLevel) {
            return;
        }
        
        Level = Math.Min(Level + levels, MaxLevel);
        Value = DefaultValue + (Level - 1) * UpgradeStep;
    }

    public bool Equals(Stat other) {
        return Chance.Equals(other.Chance) && DefaultValue.Equals(other.DefaultValue) && Level == other.Level && LimitMaxLevel.Equals(other.LimitMaxLevel) && MaxLevel == other.MaxLevel && UpgradeStep.Equals(other.UpgradeStep) && Value.Equals(other.Value);
    }

    public override bool Equals(object obj) {
        return obj is Stat other && Equals(other);
    }
}