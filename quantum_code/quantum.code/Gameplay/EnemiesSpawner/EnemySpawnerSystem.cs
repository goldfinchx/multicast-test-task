using Photon.Deterministic;
using Quantum.Core;
using Quantum.Task;

namespace Quantum.Gameplay.EnemiesSpawner;

public unsafe class EnemySpawnerSystem : SystemSignalsOnly {

    public override void OnInit(Frame frame) {
        EnemySpawnerConfig spawnerConfig = frame.FindAsset<EnemySpawnerConfig>(frame.RuntimeConfig.EnemySpawnerData);
        for (int i = 0; i < spawnerConfig.BaseEnemyCount; i++) {
            SpawnEnemy(frame);
        }
    }
    

    public void SpawnEnemy(Frame frame) {
        AssetRef enemyPrefab = GetRandomEnemyPrefab(frame);
        FPVector3 spawnPosition = GetRandomSpawnPosition(frame);
        EntityPrototype prototype = frame.FindAsset<EntityPrototype>(enemyPrefab.Id);
        EntityRef entity = frame.Create(prototype);
        
        if (frame.Unsafe.TryGetPointer<Transform3D>(entity, out Transform3D* transform)) {
            transform->Position = spawnPosition;
        }
    }
    
    private FPVector3 GetRandomSpawnPosition(Frame frame) {
        EnemySpawnerConfig spawnerConfig = frame.FindAsset<EnemySpawnerConfig>(frame.RuntimeConfig.EnemySpawnerData);
        int x = frame.Global->RngSession.Next(-spawnerConfig.MaxSpawnRadius, spawnerConfig.MaxSpawnRadius);
        int y = spawnerConfig.YLevel;
        int z = frame.Global->RngSession.Next(-spawnerConfig.MaxSpawnRadius, spawnerConfig.MaxSpawnRadius);

        return new FPVector3(x, y, z);
    }
    
    private AssetRef GetRandomEnemyPrefab(Frame frame) {
        EnemySpawnerConfig spawnerConfig = frame.FindAsset<EnemySpawnerConfig>(frame.RuntimeConfig.EnemySpawnerData);
        int randomIndex = frame.Global->RngSession.Next(0, spawnerConfig.EnemyPrefabs.Count);
        return spawnerConfig.EnemyPrefabs[randomIndex];
    }
    
    
}