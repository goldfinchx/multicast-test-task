using Photon.Deterministic;

namespace Quantum {
  partial class RuntimeConfig {
    public AssetRef EnemySpawnerData;

    partial void SerializeUserData(BitStream stream) { 
      stream.Serialize(ref EnemySpawnerData);
    }
  }
}