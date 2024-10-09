using Photon.Deterministic;
using System;

namespace Quantum {
  partial class RuntimeConfig {
    public AssetRef EnemySpawnerData;

    partial void SerializeUserData(BitStream stream) { 
      stream.Serialize(ref EnemySpawnerData);
    }
  }
}