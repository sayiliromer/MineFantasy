using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [GhostEnabledBit]
    public struct Equipment : IEnableableComponent
    {
        [GhostField]
        public Entity Owner;
    }
}