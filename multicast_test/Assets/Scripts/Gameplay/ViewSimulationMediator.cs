using Quantum;
using UniRx;
using UnityEngine;

namespace Gameplay {
    
    public class ViewSimulationMediator : MonoBehaviour {
        
        public Subject<EventBase> EventsSubject { get; private set; }

        private void Awake() {
            EventsSubject = new Subject<EventBase>();
            QuantumEvent.Subscribe<EventBase>(listener: this, handler: @event => EventsSubject.OnNext(@event));
        }

        private void OnDestroy() {
            QuantumEvent.UnsubscribeListener<EventStatUpdate>(this);
            EventsSubject.OnCompleted();
            EventsSubject.Dispose();
        }


        // todo move to each stat separtely
       // QuantumRunner.Default.Game.SendCommand(new UISetupCommand());
        


    }
}