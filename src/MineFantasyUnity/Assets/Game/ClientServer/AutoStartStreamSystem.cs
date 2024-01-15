using Mine.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Mine.ClientServer
{
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial struct AutoStartStreamSystem : ISystem
    {
        public void OnCreate(ref SystemState state) 
        { 
        }

        public void OnUpdate(ref SystemState state) 
        {
            var commandBuffer = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (id, entity) in SystemAPI.Query<RefRO<NetworkId>>().WithEntityAccess().WithNone<NetworkStreamInGame>())
            {
                commandBuffer.AddComponent<NetworkStreamInGame>(entity);
            }
            commandBuffer.Playback(state.EntityManager);
        }
    }
}
