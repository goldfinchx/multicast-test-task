using Photon.Deterministic;

namespace Quantum { 
    partial class Frame {
        public FP ElapsedTime => DeltaTime * (Number - SessionConfig.RollbackWindow);

        public bool TryGetPlayerEntity(int playerId, out EntityRef entity) {
            ComponentIterator<Player> componentIterator = GetComponentIterator<Player>();
            foreach ((EntityRef possibleEntity, Player player) in componentIterator) {
                if (player.Reference != playerId) {
                    continue;
                }

                entity = possibleEntity;
                return true;
            }
            
            entity = EntityRef.None;
            return false;
        }
        
    }
}