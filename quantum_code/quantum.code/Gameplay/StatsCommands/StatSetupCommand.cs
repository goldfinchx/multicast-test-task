using Photon.Deterministic;

namespace Quantum.Gameplay.StatsCommands;

public class StatSetupCommand : DeterministicCommand {
    public int PlayerId;
    public int StatType;
    
    public override void Serialize(BitStream stream) {
        stream.Serialize(ref PlayerId);
        stream.Serialize(ref StatType);
    }
}