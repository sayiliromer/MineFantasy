using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [InternalBufferCapacity(8)]
    public struct FloatStats : IBufferElementData
    {
        [GhostField(Quantization = 1000)] public float Value;
    }
}
