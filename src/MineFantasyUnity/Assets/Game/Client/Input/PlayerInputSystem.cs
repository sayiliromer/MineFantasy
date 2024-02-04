using Mine.ClientServer;
using Mine.Core;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine;

namespace Mine.Client
{
    [WorldSystemFilter(WorldFilters.NoThinClient)]
    [UpdateInGroup(typeof(InputGroup))]
    public partial struct PlayerInputSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            foreach (var input in SystemAPI.Query<RefRW<UnitInput>>().WithAll<GhostOwnerIsLocal>())
            {
                input.ValueRW.Direction = new float2(x, y);
            }
        }
    }
}