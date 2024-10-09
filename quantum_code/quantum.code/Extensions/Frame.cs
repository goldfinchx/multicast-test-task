using Photon.Deterministic;

namespace Quantum { 
    partial class Frame {
        public FP ElapsedTime {
            get {
                return DeltaTime * (Number - SessionConfig.RollbackWindow);
            }
        }
    }
}