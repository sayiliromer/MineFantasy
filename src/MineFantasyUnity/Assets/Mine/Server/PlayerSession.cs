using Core;
using Mine.ClientServer;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Mine.Server
{
    public struct PlayerSession : ICleanupComponentData
    {
        public Entity PlayerUnit;
    }

    [WorldSystemFilter(WorldFilters.Server)]
    public partial struct SpawnPlayerUnitSystem : ISystem
    {
        private Random _random;

        public void OnCreate(ref SystemState state)
        {
            _random = new Random(10);
            state.RequireForUpdate<GhostRepo>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var repo = SystemAPI.GetSingleton<GhostRepo>();
            var command = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (id, entity) in SystemAPI.Query<NetworkId>().WithEntityAccess().WithNone<PlayerSession>())
            {
                var player = command.Instantiate(repo.PlayerUnit);
                var randomPosition = _random.NextFloat2Direction() * _random.NextFloat() * 5;
                command.SetComponent(player, LocalTransform.FromPosition(randomPosition.x, 0, randomPosition.y));
                command.SetComponent(player, new GhostOwner
                {
                    NetworkId = id.Value
                });
                command.AddComponent(entity, new PlayerSession
                {
                    PlayerUnit = player
                });
                // var gr = command.SetBuffer<GhostGroup>(player);
                // for (int i = 0; i < 1000; i++)
                // {
                //     var intState = command.Instantiate(repo.IntState);
                //     //command.AddComponent(intState, new GhostChildEntity());
                //     // gr.Add(new GhostGroup()
                //     // {
                //     //     Value = intState
                //     // });
                // }
                // for (int i = 0; i < 1; i++)
                // {
                //     var ability = command.Instantiate(repo.TestAbility);
                //     command.SetComponent(ability, new Equipment()
                //     {
                //         Owner = player
                //     });
                // }
            }

            foreach (var (session, entity) in SystemAPI.Query<PlayerSession>().WithEntityAccess().WithNone<NetworkId>())
            {
                command.DestroyEntity(session.PlayerUnit);
                command.RemoveComponent<PlayerSession>(entity);
            }

            command.Playback(state.EntityManager);
        }
    }
}