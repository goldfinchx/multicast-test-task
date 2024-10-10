using Quantum.Gameplay.BaseMovement;
using Quantum.Gameplay.BasePlayer;
using Quantum.Gameplay.Combat;
using Quantum.Gameplay.Damage;
using Quantum.Gameplay.Death;
using Quantum.Gameplay.EnemiesSpawner;
using Quantum.Gameplay.PlayerMovement;
using Quantum.Gameplay.Stats;
using Quantum.Gameplay.Upgrade;
using Quantum.Gameplay.UpgradeCommand;

namespace Quantum {
  public static class SystemSetup {
    public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      return [
        // pre-defined core systems
        //new Core.CullingSystem2D(), 
        new Core.CullingSystem3D(),
        
        //new Core.PhysicsSystem2D(),
        new Core.PhysicsSystem3D(),

        Core.DebugCommand.CreateSystem(),

        //new Core.NavigationSystem(),
        new Core.EntityPrototypeSystem(),
        new Core.PlayerConnectedSystem(),

        // user systems go here
        new PlayerSpawnSystem(),
        new MovementSystem(),
        new PlayerMovementSystem(),
        new PlayerStatsUpdateSystem(),
        new EnemySpawnerSystem(),
        new CombatSystem(),
        new DeathSystem(),
        new DamageSystem(),
        new UpgradeCommandSystem(),
        new UpgradeSystem()
      ];
    }
  }
}
