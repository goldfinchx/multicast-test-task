using Photon.Deterministic;

namespace Quantum.Gameplay.UpgradeCommand;

public unsafe class UpgradeCommandSystem : SystemMainThread {
    public override void Update(Frame frame) {
        for (int i = 0; i < frame.PlayerCount; i++) {
            DeterministicCommand command = frame.GetPlayerCommand(i);
            if (command is not UpgradeCommand) {
                continue;
            }

            frame.Signals.OnUpgradeCommand(i);

        }
    }
    
}
