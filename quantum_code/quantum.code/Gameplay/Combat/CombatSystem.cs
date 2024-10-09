using System;
using Photon.Deterministic;
using Quantum.Physics3D;

namespace Quantum.Gameplay.Combat;

public unsafe class CombatSystem : SystemMainThreadFilter<CombatSystem.Filter> {

    public struct Filter {
        public EntityRef Entity;
        public Attacker* Attacker;
        public Transform3D* Transform;
    }

    public override void Update(Frame frame, ref Filter filter) {
        if (IsInCooldown(frame, filter.Attacker)) {
            return;
        }
        
        HitCollection3D hits = QueryEntitiesAround(frame, filter.Transform->Position, filter.Attacker->Stats.Range);
        if (hits.Count == 0) {
            return;
        }

        HitCollection3D sortedHits = SortHitEntities(frame, hits, filter.Attacker->Type);
        if (sortedHits.Count == 0) {
            return;
        }
        
        // todo damage
        
        
    }

    private HitCollection3D SortHitEntities(Frame frame, HitCollection3D hits, AttackerType attackerType) {
        hits.SortCastDistance();
        HitCollection3D sortedHits = new();
        
        for (int i = 0; i < hits.Count; i++) {
            if (sortedHits.Count >= 3) {
                break;
            }
            
            Hit3D hit = hits[i];
            EntityRef entity = hit.Entity;
            if (!frame.Has<Quantum.Health>(entity)) {
                continue;
            }

            bool skip = attackerType switch {
                AttackerType.Enemy => frame.Has<EnemyMarker>(entity),
                AttackerType.Player => frame.Has<Player>(entity),
                _ => true
            };

            if (skip) {
                continue;
            }

            sortedHits.Add(hit, frame.Context);
        }

        return sortedHits;
    }

    private bool IsInCooldown(Frame frame, Attacker* attacker) {
        return frame.ElapsedTime < attacker->LastAttackTime + attacker->Stats.Cooldown;
    }

    private HitCollection3D QueryEntitiesAround(Frame frame, FPVector3 center, FP radius) {
        Shape3D castShape = Shape3D.CreateSphere(radius);
        return frame.Physics3D.ShapeCastAll(center, FPQuaternion.Identity, castShape, FPVector3.Forward, options:QueryOptions.HitDynamics);
    }
}