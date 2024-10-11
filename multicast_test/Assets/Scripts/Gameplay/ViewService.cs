using Gameplay.UIs.Enemies;
using Quantum;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay {
    
    public class ViewService : MonoBehaviour {

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

    }
}