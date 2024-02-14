using Core;
using Mine.ClientServer;
using Unity.Entities;
using Unity.Transforms;

namespace Mine.Server
{
    [WorldSystemFilter(WorldFilters.Server)]
    public partial struct SpawnCubeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GhostRepo>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var repo = SystemAPI.GetSingleton<GhostRepo>();

            for (var i = 0; i < 1; i++)
            {
                var entity = state.EntityManager.Instantiate(repo.NetworkedCube);
                state.EntityManager.SetComponentData(entity, LocalTransform.FromPosition(i * 2, 0, 0));
            }


            // var size = 10;
            // for (int y = 0; y < size; y++)
            // {
            //     for (int x = 0; x < size; x++)
            //     {
            //         var entity1 = state.EntityManager.Instantiate(repo.UnitStats);
            //         state.EntityManager.SetComponentData(entity1, LocalTransform.FromPosition(x * 2, 0, y * 2));
            //         // state.EntityManager.SetComponentData(entity1, new GhostOwner()
            //         // {
            //         //     NetworkId = -1
            //         // });
            //     }
            // }
            state.Enabled = false;
        }
    }
}