using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct GenericTimer : IComponentData 
    {
        [GhostField] public float Remaining;
    }
}
