using Photon.Deterministic;

namespace Quantum;

public partial struct Rotation {
    
    public FP AdjustedSpeed => Speed * FP._1_20;

    
}