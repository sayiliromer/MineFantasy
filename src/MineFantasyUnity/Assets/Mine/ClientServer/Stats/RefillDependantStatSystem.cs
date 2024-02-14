// using Core;
// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Jobs;
// using Unity.Profiling;
//
// namespace Mine.ClientServer
// {
//     [WorldSystemFilter(WorldFilters.ClientServer)]
//     public partial struct RefillDependantStatSystem : ISystem
//     {
//         public NativeArray<FixedList64Bytes<float>> A;
//         public NativeArray<FixedList64Bytes<float>> B;
//         public NativeArray<FixedList64Bytes<float>> C;
//         public ProfilerMarker Pm;
//
//         public void OnCreate(ref SystemState state)
//         {
//             Pm = new ProfilerMarker("AAAA");
//             A = new NativeArray<FixedList64Bytes<float>>(1000000, Allocator.Persistent);
//             B = new NativeArray<FixedList64Bytes<float>>(1000000, Allocator.Persistent);
//             C = new NativeArray<FixedList64Bytes<float>>(1000000, Allocator.Persistent);
//
//             for (var i = 0; i < C.Length; i++)
//             {
//                 var f1 = C[i];
//                 f1.Add(1);
//                 C[i] = f1;
//
//
//                 var f2 = B[i];
//                 f2.Add(1);
//                 B[i] = f2;
//
//
//                 var f3 = A[i];
//                 f3.Add(1);
//                 A[i] = f1;
//             }
//         }
//
//         [BurstCompile]
//         public void OnUpdate(ref SystemState state)
//         {
//             return;
//
//             // state.Dependency = new RefillJob()
//             // {
//             //     Dt = SystemAPI.Time.DeltaTime
//             // }.Schedule(state.Dependency);
//
//
//             //using (Pm.Auto())
//             {
//                 Pm.Begin();
//                 //Test();
//                 new SomeJob
//                 {
//                     A = A,
//                     B = B,
//                     C = C
//                 }.Run();
//
//                 new RefillJob
//                 {
//                     Dt = SystemAPI.Time.DeltaTime
//                 }.Run();
//
//                 Pm.End();
//             }
//         }
//
//         public void OnDestroy(ref SystemState state)
//         {
//             A.Dispose();
//             B.Dispose();
//             C.Dispose();
//         }
//
//         [BurstCompile]
//         public struct SomeJob : IJob
//         {
//             public NativeArray<FixedList64Bytes<float>> A;
//             public NativeArray<FixedList64Bytes<float>> B;
//             public NativeArray<FixedList64Bytes<float>> C;
//
//             public void Execute()
//             {
//                 for (var i = 0; i < C.Length; i++)
//                 {
//                     var f1 = C[i];
//                     var f2 = A[i];
//                     var f3 = B[i];
//                     f1[0] = f2[0] * f3[0];
//                 }
//             }
//         }
//
//         [BurstCompile]
//         public void Test()
//         {
//             for (var i = 0; i < C.Length; i++)
//             {
//                 var f1 = C[i];
//                 var f2 = A[i];
//                 var f3 = B[i];
//                 f1[0] = f2[0] * f3[0];
//             }
//         }
//
//         // [BurstCompile]
//         // [WithAll(typeof(Simulate))]
//         // public partial struct RefillJobB : IJobEntity
//         // {
//         //     public float Dt;
//         //
//         //     private void Execute(ref SomeListData indexes)
//         //     {
//         //         for (int i = 0; i < indexes.List.Length; i++)
//         //         {
//         //             var indexData = indexes.List[i];
//         //             //var element = stats[indexData.ToRefillIndex];
//         //             //ref var element = ref stats.ElementAt(indexData.ToRefillIndex);
//         //             // var finalMaxValue = GetFinalValueDynamic(stats, indexData.ToRefillIndex);
//         //             // var finalAmountValue = GetFinalValueBase(stats, indexData.AmountIndex);
//         //             // if (element.Value > finalMaxValue) continue;
//         //             // element.Value += finalAmountValue * Dt;
//         //             // if (element.Value < finalMaxValue) continue;
//         //             // element.Value = finalMaxValue;
//         //         }
//         //     }
//         //
//         //     // private float GetFinalValueBase(DynamicBuffer<FloatStat> buffer, int baseIndex)
//         //     // {
//         //     //     return (buffer[baseIndex].Value + buffer[baseIndex + 1].Value) * (1f + buffer[baseIndex + 2].Value);
//         //     // }
//         //     //
//         //     // private float GetFinalValueDynamic(DynamicBuffer<FloatStat> buffer, int baseIndex)
//         //     // {
//         //     //     return (buffer[baseIndex - 3].Value + buffer[baseIndex - 2].Value) * (1f + buffer[baseIndex -1].Value);
//         //     // }
//         // }
//
//         [BurstCompile]
//         [WithAll(typeof(Simulate))]
//         public partial struct RefillJob : IJobEntity
//         {
//             public float Dt;
//
//             private void Execute(ref RefillIndexListData indexes, ref FloatStatList stats)
//             {
//                 for (var i = 0; i < indexes.List.Length; i++)
//                 {
//                     var indexData = indexes.List[i];
//                     var element = stats.List[indexData.ToRefillIndex];
//                     //ref var element = ref stats.ElementAt(indexData.ToRefillIndex);
//                     var finalMaxValue = GetFinalValueDynamic(stats, indexData.ToRefillIndex);
//                     var finalAmountValue = GetFinalValueBase(stats, indexData.AmountIndex);
//                     if (element < finalMaxValue)
//                     {
//                         element += finalAmountValue * Dt;
//                         if (element > finalMaxValue) element = finalMaxValue;
//                     }
//
//                     stats.List[indexData.ToRefillIndex] = element;
//                 }
//             }
//
//             private float GetFinalValueBase(in FloatStatList buffer, int baseIndex)
//             {
//                 return (buffer.List[baseIndex] + buffer.List[baseIndex + 1]) * (1f + buffer.List[baseIndex + 2]);
//             }
//
//             private float GetFinalValueDynamic(in FloatStatList buffer, int baseIndex)
//             {
//                 return (buffer.List[baseIndex - 3] + buffer.List[baseIndex - 2]) * (1f + buffer.List[baseIndex - 1]);
//             }
//         }
//     }
//
//     public struct RefillIndexListData : IComponentData
//     {
//         public FixedList32Bytes<IndexTuple> List;
//     }
//
//     public struct IndexTuple
//     {
//         public byte AmountIndex;
//         public byte ToRefillIndex;
//     }
// }