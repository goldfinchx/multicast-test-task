using Quantum.Collections;

namespace Quantum.Gameplay.Stats;

public unsafe class PlayerStatsUpdateSystem : SystemMainThreadFilter<PlayerStatsUpdateSystem.Filter>, ISignalOnPlayerSpawn {
    
    public struct Filter {
        public EntityRef Entity;
        public PlayerStats* Stats;
        public Movement* Movement;
        public Attacker* Attacker;
        public Player* Player;
    }
    
    public override void Update(Frame frame, ref Filter filter) {
       // throw new System.NotImplementedException();
    }

    public void OnPlayerSpawn(Frame frame, EntityRef player) {
        if (!frame.Unsafe.TryGetPointer(player, out PlayerStats* stats)) {
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(player, out Movement* movement)) {
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(player, out Attacker* attacker)) {
            return;
        }

        QEnumDictionary<StatType,Stat> values = frame.ResolveDictionary(stats->Values);
        movement->Speed = values[StatType.MovementSpeed].DefaultValue;
        attacker->Stats.Damage = values[StatType.AttackDamage].DefaultValue.AsInt;
        attacker->Stats.Range = values[StatType.AttackRange].DefaultValue.AsInt;
    }
}