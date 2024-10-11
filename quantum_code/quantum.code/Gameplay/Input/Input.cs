using Photon.Deterministic;

namespace Quantum;

public partial struct Input {
    public FPVector2 Movement {
        get {
            if (EncodedMovement == default) {
                return default;
            }
    
            int angle = (EncodedMovement - 1) * 2;
            return FPVector2.Rotate(FPVector2.Up, angle * FP.Deg2Rad);
        }
        set {
            if (value == default) {
                EncodedMovement = default;
                return;
            }
    
            FP angle = FPVector2.RadiansSigned(FPVector2.Up, value) * FP.Rad2Deg;
            angle = (angle + 360) % 360 / 2 + 1;
            EncodedMovement = (byte) angle.AsInt;
        }
    }
    
    public FPVector2 Rotation {
        get {
            if (EncodedRotation == default) {
                return default;
            }
    
            int angle = (EncodedRotation - 1) * 2;
            return FPVector2.Rotate(FPVector2.Up, angle * FP.Deg2Rad);
        }
        set {
            if (value == default) {
                EncodedRotation = default;
                return;
            }
    
            FP angle = FPVector2.RadiansSigned(FPVector2.Up, value) * FP.Rad2Deg;
            angle = (angle + 360) % 360 / 2 + 1;
            EncodedRotation = (byte) angle.AsInt;
        }
    }
    
}