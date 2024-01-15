using Mine.ClientServer;
using Mine.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Mine.Server
{
    [RequireMatchingQueriesForUpdate]
    [WorldSystemFilter(WorldFilters.Server)]
    public partial struct SpawnCubeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var repo = SystemAPI.GetSingleton<GhostRepo>();

            for (int i = 0; i < 1; i++)
            {
                var entity = state.EntityManager.Instantiate(repo.NetworkedCube);
                state.EntityManager.SetComponentData(entity, LocalTransform.FromPosition(i * 2, 0, 0));
            }

            state.Enabled = false;
        }
    }
}
