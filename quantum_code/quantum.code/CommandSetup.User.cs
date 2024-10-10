using System;
using System.Collections.Generic;
using Photon.Deterministic;
using Quantum.Gameplay.PlayerSetup;
using Quantum.Gameplay.UICommand;
using Quantum.Gameplay.UpgradeCommand;

namespace Quantum {
  public static partial class DeterministicCommandSetup {
    static partial void AddCommandFactoriesUser(ICollection<IDeterministicCommandFactory> factories, RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      // user commands go here
      factories.Add(new UpgradeCommand());
      factories.Add(new UISetupCommand());
    }
  }
}
