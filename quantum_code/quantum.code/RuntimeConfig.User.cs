using Photon.Deterministic;

namespace Quantum {
  partial class RuntimeConfig {
    public AssetRef EnemySpawnerData;
    public AssetRef DefaultHeroStats;

    partial void SerializeUserData(BitStream stream) { 
      stream.Serialize(ref EnemySpawnerData);
      stream.Serialize(ref DefaultHeroStats);
    }
  }
}