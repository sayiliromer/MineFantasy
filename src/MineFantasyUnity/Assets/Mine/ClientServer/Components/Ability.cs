using Unity.Entities;

namespace Mine.ClientServer
{
    public struct Ability : IComponentData
    {
    }
    
    public struct EntitySpawnerAbility : IComponentData
    {
        public Entity Prefab;
    }
}