using Quantum.Gameplay.BaseMovement;
using Quantum.Gameplay.BaseRotation;
using Quantum.Gameplay.Combat;
using Quantum.Gameplay.Commands;
using Quantum.Gameplay.Damage;
using Quantum.Gameplay.Death;
using Quantum.Gameplay.EnemiesSpawner;
using Quantum.Gameplay.Events;
using Quantum.Gameplay.Health;
using Quantum.Gameplay.PlayerConnections;
using Quantum.Gameplay.PlayerMovement;
using Quantum.Gameplay.PlayerRotation;
using Quantum.Gameplay.PlayerSetup;
using Quantum.Gameplay.StatsSetup;
using Quantum.Gameplay.StatsUpdate;
using Quantum.Gameplay.StatsUpgrade;

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
        new EventsSystem(),
        new CommandsSystem(),
        new PlayerConnectionsSystem(),
        new PlayerSpawnSystem(),
        new StatsSetupSystem(),
        new StatsUpdateSystem(),
        new StatsUpgradeSystem(),
        new EnemySpawnerSystem(),
        new DamageSystem(),
        new CombatSystem(),
        new HealthSetSystem(),
        new DeathSystem(),
        new MovementSystem(),
        new PlayerMovementSystem(),
        new RotationSystem(),
        new PlayerRotationSystem()
      ];
    }
  }
}
