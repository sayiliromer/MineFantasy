using Unity.Entities;

namespace Mine.ClientServer
{
    public struct PrefabContainer : IComponentData
    {
        public Entity Prefab;
    }
}