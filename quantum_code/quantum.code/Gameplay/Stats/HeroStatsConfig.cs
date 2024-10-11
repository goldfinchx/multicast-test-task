using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum;

partial class HeroStatsConfig {

    public Stat AttackDamage = new() {
        Type = StatType.AttackDamage,
        DefaultValue = 10,
        Level = 1,
        UpgradeStep = 10,
        Chance = 30
    };
    
    public Stat AttackRange = new() {
        Type = StatType.AttackRange,
        DefaultValue = 2,
        UpgradeStep = 1,
        Chance = 10
    };
    
    public Stat MovementSpeed = new() {
        Type = StatType.MovementSpeed,
        DefaultValue = 1,
        UpgradeStep = FP._0_50,
        Chance = 60
    };
    
    public QList<Stat> GetStats(Frame frame) {
        QList<Stat> stats = frame.AllocateList<Stat>(3);
        AttackDamage.Level = 1;
        AttackRange.Level = 1;
        MovementSpeed.Level = 1;
        
        AttackDamage.Value = AttackDamage.DefaultValue;
        AttackRange.Value = AttackRange.DefaultValue;
        MovementSpeed.Value = MovementSpeed.DefaultValue;
        
        stats.Add(AttackDamage);
        stats.Add(AttackRange);
        stats.Add(MovementSpeed);
        return stats;
    }
    
    
}