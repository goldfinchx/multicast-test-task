using Photon.Deterministic;

namespace Quantum;

public partial struct Movement {
    
    public FP AdjustedSpeed => Speed * FP._1_20;
    
}