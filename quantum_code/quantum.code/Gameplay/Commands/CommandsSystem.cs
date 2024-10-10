using Photon.Deterministic;
using Quantum.Gameplay.UICommand;

namespace Quantum.Gameplay.Commands;

public class CommandsSystem : SystemMainThread {
    public override void Update(Frame frame) {
        for (int i = 0; i < frame.PlayerCount; i++) {
            DeterministicCommand command = frame.GetPlayerCommand(i);
            switch (command) {
                case UpgradeCommand.UpgradeCommand:
                    frame.Signals.OnUpgradeCommand(i);
                    continue;
                case UISetupCommand:
                    frame.Signals.OnUISetupCommand(i);
                    continue;
                default: continue;
            }
        }
    }
    
}