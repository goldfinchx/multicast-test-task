namespace Quantum.Gameplay.BasePlayer;

public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerDataSet {
    
    public void OnPlayerDataSet(Frame frame, PlayerRef playerRef) {
        RuntimePlayer data = frame.GetPlayerData(playerRef);
        EntityPrototype prototype = frame.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
        EntityRef entity = frame.Create(prototype);
        
        Player playerComponent = new() { Reference = playerRef };
        frame.Set(entity, playerComponent);
        
        if (frame.Unsafe.TryGetPointer(entity, out Transform3D* transform)) {
            transform->Position.X = (int) playerRef;
        }
    }
}