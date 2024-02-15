using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [GhostEnabledBit]
    public struct Equipment : IComponentData
    {
        [GhostField]
        public Entity Owner;
    }
}