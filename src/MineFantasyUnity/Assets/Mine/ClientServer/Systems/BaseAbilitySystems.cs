using Core;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;

namespace Mine.ClientServer
{
    // [WorldSystemFilter(WorldFilters.ClientServer)]
    // [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    // [UpdateAfter(typeof(AbilityBeginSystem))]
    // [UpdateBefore(typeof(AbilityEndSystem))]
    // [BurstCompile]
    // public partial struct MeleeWeaponSystem : ISystem
    // {
    //     private ComponentLookup<LocalTransform> _transforms;
    //
    //     public void OnCreate(ref SystemState state)
    //     {
    //         _transforms = state.GetComponentLookup<LocalTransform>();
    //     }
    //
    //     public void OnUpdate(ref SystemState state)
    //     {
    //         _transforms.Update(ref state);
    //         var dt = SystemAPI.Time.DeltaTime;
    //         state.Dependency = new SetWeaponPositionSystem(_transforms).ScheduleParallel(state.Dependency);
    //     }
    //     
    //     [WithAll(typeof(Simulate))]
    //     [WithAll(typeof(MeleeWeapon))]
    //     [BurstCompile]
    //     private partial struct SetWeaponPositionSystem : IJobEntity
    //     {
    //         [NativeDisableParallelForRestriction]
    //         private ComponentLookup<LocalTransform> _transforms;
    //         public SetWeaponPositionSystem(ComponentLookup<LocalTransform> transforms) : this()
    //         {
    //             _transforms = transforms;
    //         }
    //
    //         void Execute(in Entity entity,in Equipment equipment, in Timer timer, in IntState state)
    //         {
    //             if (!_transforms.TryGetComponent(equipment.Owner, out var ownerTransform)) return;
    //             var transform = _transforms.GetRefRW(entity);
    //             transform.ValueRW.Position = ownerTransform.Position;
    //             transform.ValueRW.Rotation = ownerTransform.Rotation;
    //         }
    //     }
    // }
    //
    // [WorldSystemFilter(WorldFilters.ClientServer)]
    // [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    // [UpdateAfter(typeof(AbilityBeginSystem))]
    // public partial struct AbilityEndSystem : ISystem
    // {
    //     public void OnUpdate(ref SystemState state)
    //     {
    //         var dt = SystemAPI.Time.DeltaTime;
    //         state.Dependency = new CoolingToReadyJob().ScheduleParallel(state.Dependency);
    //         state.Dependency = new ActiveJob(dt).ScheduleParallel(state.Dependency);
    //         //Trigger point: ability entering to cooling phase
    //         //state.CompleteDependency();
    //         //LogStateChange(ref state);
    //         
    //     }
    //
    //     private void LogStateChange(ref SystemState state)
    //     {
    //         state.CompleteDependency();
    //         foreach (var itemState in SystemAPI.Query<IntState>().WithAll<Simulate>().WithEntityAccess())
    //         {
    //             if (itemState.Item1 != ActiveItemState.ActiveToCooling) continue;
    //             var name = state.EntityManager.GetName(itemState.Item2);
    //             Debug.Log($"{name}:{state.World.Name} : Entered {(ActiveItemState)itemState.Item1}");
    //         }
    //     }
    //
    //     [WithAll(typeof(Simulate))]
    //     [WithAll(typeof(Item))]
    //     private partial struct ActiveJob : IJobEntity
    //     {
    //         private float _dt;
    //         
    //         public ActiveJob(float dt) : this()
    //         {
    //             _dt = dt;
    //         }
    //
    //         void Execute(ref Timer timer, ref IntState state, in AbilityTimings timings, in Equipment equipment)
    //         {
    //             if (state != ActiveItemState.Active) return;
    //             timer.Value += _dt;
    //             if (timer.Value >= timings.Active)
    //             {
    //                 timer.Value -= timings.Active;
    //                 state = ActiveItemState.ActiveToCooling;
    //             }
    //         }
    //     }
    //     
    //     [WithAll(typeof(Simulate))]
    //     [WithAll(typeof(Item))]
    //     private partial struct CoolingToReadyJob : IJobEntity
    //     {
    //         void Execute(ref Timer timer, ref IntState state, in AbilityTimings timings, in Equipment equipment)
    //         {
    //             if (state != ActiveItemState.CoolingToReady) return;
    //             
    //             state = ActiveItemState.Ready;
    //         }
    //     }
    // }
    //
    // [WorldSystemFilter(WorldFilters.ClientServer)]
    // [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    // public partial struct AbilityBeginSystem : ISystem
    // {
    //     public void OnUpdate(ref SystemState state)
    //     {
    //         var dt = SystemAPI.Time.DeltaTime;
    //         //Countdown cooldowns
    //         
    //         state.Dependency = new CoolingJob(dt).ScheduleParallel(state.Dependency);
    //         //Trigger point: ability entering to ready state
    //         //LogStateChange(ref state);
    //     }
    //
    //     private void LogStateChange(ref SystemState state)
    //     {
    //         state.CompleteDependency();
    //         foreach (var itemState in SystemAPI.Query<IntState>().WithAll<Simulate>().WithEntityAccess())
    //         {
    //             if (itemState.Item1 != ActiveItemState.CoolingToReady) continue;
    //             var name = state.EntityManager.GetName(itemState.Item2);
    //             Debug.Log($"{name}:{state.World.Name} : Entered {(ActiveItemState)itemState.Item1}");
    //         }
    //     }
    //     
    //     [WithAll(typeof(Simulate))]
    //     [WithAll(typeof(Item))]
    //     private partial struct CoolingJob : IJobEntity
    //     {
    //         private float _dt;
    //         public CoolingJob(float dt) : this()
    //         {
    //             _dt = dt;
    //         }
    //         
    //         void Execute(ref Timer timer, ref IntState state, in AbilityTimings timings, in Equipment equipment)
    //         {
    //             if (state == ActiveItemState.ActiveToCooling)
    //             {
    //                 state = ActiveItemState.Cooling;
    //             }
    //             if(state != ActiveItemState.Cooling) return;
    //             timer.Value += _dt;
    //             if (timer.Value >= timings.Cooldown)
    //             {
    //                 timer.Value = 0;
    //                 state = ActiveItemState.CoolingToReady;
    //             }
    //         }
    //     }
    //     
    // }
}