using Photon.Deterministic;

namespace Quantum.Gameplay.UICommand;

public class UISetupCommand : DeterministicCommand {
    private int playerId;

    public override void Serialize(BitStream stream) {
        stream.Serialize(ref playerId);
    }
}