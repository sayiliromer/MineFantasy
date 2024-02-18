using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [InternalBufferCapacity(8)]
    public struct EntityContainer : IBufferElementData
    {
        [GhostField] public Entity Value;
    }
}