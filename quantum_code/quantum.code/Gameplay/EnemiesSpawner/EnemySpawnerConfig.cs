using System;
using System.Collections.Generic;

namespace Quantum;

partial class EnemySpawnerConfig {

    public int BaseEnemyCount = 4;
    public int MaxSpawnRadius = 10;
    public int YLevel = 5;
    public List<AssetRef> EnemyPrefabs = new();
}