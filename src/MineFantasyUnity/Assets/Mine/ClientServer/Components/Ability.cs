using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct Ability : IComponentData
    {
    }
    
    public struct PrefabContainer : IComponentData
    {
        public Entity Prefab;
    }
    
    [InternalBufferCapacity(64)]
    public struct IdInventory : IBufferElementData
    {
        [GhostField] public short Id;
        [GhostField] public short Stack;
    }
}