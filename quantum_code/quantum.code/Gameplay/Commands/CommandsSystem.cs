using Photon.Deterministic;
using Quantum.Gameplay.StatsCommands;

namespace Quantum.Gameplay.Commands;

public class CommandsSystem : SystemMainThread {
    public override void Update(Frame frame) {
        for (int i = 0; i < frame.PlayerCount; i++) {
            DeterministicCommand command = frame.GetPlayerCommand(i);
            switch (command) {
                case UpgradeCommand.UpgradeCommand:
                    frame.Signals.OnUpgradeCommand(i);
                    continue;
                case StatSetupCommand setupCommand:
                    frame.Signals.OnStatSetupCommand(i, setupCommand.StatType);
                    continue;
                default: continue;
            }
        }
    }
    
}