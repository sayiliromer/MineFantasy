using Core;
using Unity.Entities;

namespace Mine.ClientServer
{
    [WorldSystemFilter(WorldFilters.ClientServer)]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct LoadPrefabWorldSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            const string scenePath = "Assets/Scenes/SubSceneCarrier/PrefabRepoScene.unity";
            if (EcsUtility.IsSceneLoadingOrLoaded(ref state, scenePath)) return;
            EcsUtility.LoadScene(ref state, scenePath);
        }
    }
}