namespace Quantum.Gameplay.StatsUpgrade;

public unsafe class StatsUpgradeSystem : SystemSignalsOnly, ISignalOnUpgradeCommand {
    
    public void OnUpgradeCommand(Frame frame, int player) {
        Log.Info("Upgrade command received");
        if (!frame.TryGetPlayerEntity(player, out EntityRef playerEntity)) {
            Log.Error("Player entity not found, when receiving upgrade command!");
            return;
        }
        
        if (!frame.Unsafe.TryGetPointer(playerEntity, out PlayerStats* playerStats)) {
            Log.Error("PlayerStats component not found on Player entity!");
            return;
        }

        Stat* randomizedStat = playerStats->GetRandomStat(frame);
        if (randomizedStat is null) {
            return;
        }
        
        randomizedStat->Upgrade();
        frame.Signals.OnStatUpgrade(playerEntity, *randomizedStat);
    }
}