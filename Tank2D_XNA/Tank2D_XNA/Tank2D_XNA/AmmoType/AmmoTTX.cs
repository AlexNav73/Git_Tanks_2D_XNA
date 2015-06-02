using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tank2D_XNA.AmmoType
{
    [DataContract]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct AmmoTTX
    {
        [DataMember] public int MinDamage;
        [DataMember] public int MaxDamage;
        [DataMember] public int MaxDistanse;
        [DataMember] public int Speed;
    }
}
