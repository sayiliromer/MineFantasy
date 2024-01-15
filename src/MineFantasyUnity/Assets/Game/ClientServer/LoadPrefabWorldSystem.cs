using Mine.Core;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using Hash128 = Unity.Entities.Hash128;

namespace Mine.ClientServer
{
    [WorldSystemFilter(WorldFilters.ClientServer)]
    [UpdateInGroup(typeof(MineInitGruop))]
    public partial struct LoadPrefabWorldSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            const string Scene = "Assets/Scenes/SubSceneCarrier/PrefabRepoScene.unity";
            if (IsSceneLoadingOrLoaded(ref state, Scene)) return;
            LoadScene(ref state, Scene);
        }

        public static Entity LoadScene(ref SystemState systemState, string path)
        {
            var sceneGuid = GetSceneGuid(path);
            return SceneSystem.LoadSceneAsync(systemState.WorldUnmanaged, sceneGuid);
        }

        public static Hash128 GetSceneGuid(string path)
        {
            ref var systemState = ref World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<SceneSystem>();
            return SceneSystem.GetSceneGUID(ref systemState, path);
        }

        public static bool IsSceneLoadingOrLoaded(ref SystemState systemState, string path)
        {
            var sceneGuid = GetSceneGuid(path);
            var sceneEntity = SceneSystem.GetSceneEntity(systemState.WorldUnmanaged, sceneGuid);
            
            return sceneEntity != Entity.Null;
        }
    }
}
