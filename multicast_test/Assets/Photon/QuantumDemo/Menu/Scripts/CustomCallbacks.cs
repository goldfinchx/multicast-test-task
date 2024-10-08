using Quantum;
using UnityEngine;

public class CustomCallbacks : QuantumCallbacks {

  [SerializeField] private RuntimePlayer player;

  public override void OnGameStart(QuantumGame game) {
    if (game.Session.IsPaused) {
      return;
    }

    foreach (int localPlayer in game.GetLocalPlayers()) {
      Debug.Log("CustomCallbacks - sending player: " + localPlayer);
      game.SendPlayerData(localPlayer, player);
    }
  }

  public override void OnGameResync(QuantumGame game) {
    Debug.Log("Detected Resync. Verified tick: " + game.Frames.Verified.Number);
  }
}

