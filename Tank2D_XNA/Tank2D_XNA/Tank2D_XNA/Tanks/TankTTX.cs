using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tank2D_XNA.Tanks
{
    [DataContract]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct TankTTX
    {
        [DataMember] public int Speed;
        [DataMember] public int RotationSpeed;
        [DataMember] public int HP;
        [DataMember] public double ReloadTime;
        [DataMember] public int Overlook;
        [DataMember] public string PiercingAmmoName;
        [DataMember] public int TurretCentrX;
    }
}
