using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace Core
{
    public static class EcsExtensions
    {
        public static Entity GetSingletonEntity<T>(this EntityManager manager) where T : unmanaged, IComponentData
        {
            var query = manager.CreateEntityQuery(typeof(T));
            var array = query.ToEntityArray(Allocator.Temp);
            var entity = array.FirstOrDefault();
            array.Dispose();

            return entity;
        }

        public static T? GetSingleton<T>(this EntityManager manager) where T : unmanaged, IComponentData
        {
            var query = manager.CreateEntityQuery(typeof(T));
            var array = query.ToComponentDataArray<T>(Allocator.Temp);
            T? data = null;
            if (array.Length > 0) data = array[0];
            array.Dispose();
            return data;
        }

        public static T? GetSingleton<T>(this World world) where T : unmanaged, IComponentData
        {
            return world.EntityManager.GetSingleton<T>();
        }

        public static Entity GetSingletonEntity<T>(this World world) where T : unmanaged, IComponentData
        {
            return GetSingletonEntity<T>(world.EntityManager);
        }

        public static void DestroyWorld(this World world)
        {
            world.QuitUpdate = true;
            ScriptBehaviourUpdateOrder.RemoveWorldFromCurrentPlayerLoop(world);
            world.Dispose();
        }
    }
}