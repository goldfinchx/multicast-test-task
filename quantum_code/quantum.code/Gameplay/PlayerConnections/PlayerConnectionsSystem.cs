using System.Diagnostics;

namespace Quantum.Gameplay.PlayerConnections;

public class PlayerConnectionsSystem : SystemSignalsOnly, ISignalOnPlayerDataSet {
    public void OnPlayerDataSet(Frame frame, PlayerRef playerRef) {
        if (frame.PlayerCount < playerRef) {
            return;
        }

        frame.Signals.OnAllPlayersConnected();
    }
}