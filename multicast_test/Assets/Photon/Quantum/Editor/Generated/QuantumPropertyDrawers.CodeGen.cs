// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
// </auto-generated>

namespace Quantum.Editor {
  using Quantum;
  using UnityEngine;
  using UnityEditor;

  [CustomPropertyDrawer(typeof(AssetRefEnemySpawnerConfig))]
  public class AssetRefEnemySpawnerConfigPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      AssetRefDrawer.DrawAssetRefSelector(position, property, label, typeof(EnemySpawnerConfigAsset));
    }
  }

  [CustomPropertyDrawer(typeof(AssetRefHeroStatsConfig))]
  public class AssetRefHeroStatsConfigPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      AssetRefDrawer.DrawAssetRefSelector(position, property, label, typeof(HeroStatsConfigAsset));
    }
  }

  [CustomPropertyDrawer(typeof(Quantum.Prototypes.AttackerType_Prototype))]
  [CustomPropertyDrawer(typeof(Quantum.Prototypes.StatType_Prototype))]
  [CustomPropertyDrawer(typeof(Quantum.Prototypes.InputButtons_Prototype))]
  partial class PrototypeDrawer {}
}
