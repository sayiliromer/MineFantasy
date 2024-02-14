using Unity.Entities;
using Unity.Scenes;

namespace Core
{
    public static class EcsUtility
    {
        public static World GetWorld(WorldFlags worldFlag)
        {
            foreach (var world in World.All)
                if ((world.Flags & worldFlag) == worldFlag)
                    return world;

            return null;
        }

        public static World GetWorld(string name)
        {
            foreach (var world in World.All)
                if (world.Name == name)
                    return world;

            return null;
        }

        public static Entity LoadScene(ref SystemState systemState, string path)
        {
            var sceneGuid = GetSceneGuid(path);
            return SceneSystem.LoadSceneAsync(systemState.WorldUnmanaged, sceneGuid);
        }

        public static Hash128 GetSceneGuid(string path)
        {
            ref var systemState =
                ref World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<SceneSystem>();
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