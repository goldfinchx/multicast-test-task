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
        
        HitCollection3D* sortedHits = SortHitEntities(frame, hits, filter.Attacker);
        if (sortedHits->Count == 0) {
            return;
        }
        
        for (int i = 0; i < sortedHits->Count; i++) {
            Hit3D* hit = &sortedHits->HitsBuffer[i];
            frame.Signals.OnAttack(hit->Entity, filter.Entity, filter.Attacker->Stats.Damage);
        }
        
        filter.Attacker->LastAttackTime = frame.ElapsedTime;
        frame.Physics3D.FreePersistentHitCollection3D(sortedHits);
    }

    private HitCollection3D* SortHitEntities(Frame frame, HitCollection3D hits, Attacker* attacker) {
        hits.SortCastDistance();
        HitCollection3D* sortedHits = frame.Physics3D.AllocatePersistentHitCollection3D(3);

        for (int i = 0; i < hits.Count; i++) {
            if (sortedHits->Count >= attacker->Stats.ConcurrentAttacks) {
                break;
            }

            Hit3D hit = hits[i];
            EntityRef entity = hit.Entity;
            if (!frame.Has<Quantum.Health>(entity)) {
                continue;
            }

            bool skip = attacker->Type switch {
                AttackerType.Enemy => frame.Has<EnemyMarker>(entity),
                AttackerType.Player => frame.Has<Player>(entity),
                _ => true
            };

            if (skip) {
                continue;
            }

            sortedHits->Add(hit, frame.Context);
        }

        return sortedHits;
    }

    private bool IsInCooldown(Frame frame, Attacker* attacker) {
        return frame.ElapsedTime < attacker->LastAttackTime + attacker->Stats.Cooldown;
    }

    private HitCollection3D QueryEntitiesAround(Frame frame, FPVector3 center, FP radius) {
        Shape3D castShape = Shape3D.CreateSphere(radius);
        HitCollection3D queryEntitiesAround = frame.Physics3D.OverlapShape(center, FPQuaternion.Identity, castShape);
        Log.Debug("QueryEntitiesAround count=" + queryEntitiesAround.Count);
        return queryEntitiesAround;
    }
}