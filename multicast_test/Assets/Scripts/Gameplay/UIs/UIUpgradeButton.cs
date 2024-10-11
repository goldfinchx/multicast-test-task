
using Quantum.Gameplay.UpgradeCommand;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UIs {
    [RequireComponent(typeof(Button))]
    public class UIUpgradeButton : MonoBehaviour {
        
        private Button button;

        private void Awake() {
            button = GetComponent<Button>();
        }

        private void OnEnable() {
            button.OnClickAsObservable()
                .Subscribe(_ => SendUpgradeCommand());
        }

        public void SendUpgradeCommand() {
            QuantumRunner.Default.Game.SendCommand(new UpgradeCommand());
        }

    }
}