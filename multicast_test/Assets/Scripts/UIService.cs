using Quantum.Gameplay.UpgradeCommand;
using UnityEngine;

public class UIService : MonoBehaviour {


    public void SendUpgradeCommand() {
        QuantumRunner.Default.Game.SendCommand(new UpgradeCommand());
    }
    
    
}