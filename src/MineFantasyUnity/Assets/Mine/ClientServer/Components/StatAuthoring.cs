// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Sirenix.OdinInspector;
// using Unity.Entities;
// using UnityEngine;
//
// namespace Mine.ClientServer
// {
//     [Serializable]
//     public class StatAuthoringEntry
//     {
//         [ReadOnly]
//         public byte Id;
//         [InlineProperty,HideLabel, ShowIf("IsInt")]
//         public StatAuthoringValue<int> IntValue;
//         [InlineProperty,HideLabel, ShowIf("IsFloat")]
//         public StatAuthoringValue<float> FloatValue;
//         [ValueDropdown("GetStatNames"),ShowInInspector,PropertyOrder(-1)]
//         public string Type
//         {
//             get
//             {
//                 var instance = StatConfigContainer.GetInstance();
//                 return instance.GetStat(Id).Name;
//             }
//             set
//             {
//                 var instance = StatConfigContainer.GetInstance();
//                 var stat = instance.GetStat(value);
//                 Id = stat?.Id ?? 0;
//             }
//         }
//
//         public static IEnumerable<string> GetStatNames()
//         {
//             var instance = StatConfigContainer.GetInstance();
//             return instance.Stats.Select(s=>s.Name);
//         }
//
//         
//         public bool IsInt()
//         {
//             var instance = StatConfigContainer.GetInstance();
//             var stat = instance.GetStat(Id);
//             return stat != null && stat.ValueType == StatValueType.Int;
//         }
//         
//         public bool IsFloat()
//         {
//             var instance = StatConfigContainer.GetInstance();
//             var stat = instance.GetStat(Id);
//             return stat != null && stat.ValueType == StatValueType.Float;
//         }
//
//         public StatConfig GetConfig()
//         {
//             var instance = StatConfigContainer.GetInstance();
//             var stat = instance.GetStat(Id);
//             return stat;
//         }
//     }
//
//     [Serializable]
//     public struct StatAuthoringValue<T>
//     {
//         public T Value;
//         public T Min;
//         public T Max;
//     }
//
//     public class StatAuthoring : MonoBehaviour
//     {
//         public List<StatAuthoringEntry> Stats;
//         private List<StatAuthoringEntry> _sortedStats = new();
//
//         public List<StatAuthoringEntry> GetSortedStats()
//         {
//             if (_sortedStats == null)
//             {
//                 _sortedStats = new List<StatAuthoringEntry>();
//             }
//             _sortedStats.Clear();
//             for (int i = 0; i < Stats.Count; i++)
//             {
//                 _sortedStats.Add(Stats[i]);
//             }
//             _sortedStats.Sort(Sorter);
//             return _sortedStats;
//         }
//         
//         private int Sorter(StatAuthoringEntry x, StatAuthoringEntry y)
//         {
//             return x.Id.CompareTo(y.Id);
//         }
//
//         public byte GetBaseStatValueIndex(byte statId)
//         {
//             var sorted = GetSortedStats();
//             byte indexSoFar = 0;
//             for (int i = 0; i < sorted.Count; i++)
//             {
//                 var statEntry = sorted[i];
//                 var config = statEntry.GetConfig();
//                 if (statEntry.Id == statId)
//                 {
//                     return indexSoFar;
//                 }
//                 
//                 if (config.IsDynamic)
//                 {
//                     indexSoFar += StatConfig.DynamicParameterCount;
//                 }
//                 else
//                 {
//                     indexSoFar += StatConfig.StaticParameterCount;
//                 }
//             }
//
//             return byte.MaxValue;
//         }
//
//         public byte GetDynamicStatValueIndex(byte statId)
//         {
//             return (byte)(GetBaseStatValueIndex(statId) + 3);
//         }
//         
//         public class StatAuthoringBaker : Baker<StatAuthoring>
//         {
//             public override void Bake(StatAuthoring authoring)
//             {
//                 var statsContainer = StatConfigContainer.GetInstance();
//                 DependsOn(statsContainer);
//                 if(statsContainer == null) return;
//                 
//                 var entity = GetEntity(TransformUsageFlags.None);
//                 var sortedStats = authoring.GetSortedStats();
//
//                 FloatStatList floatStats = default;
//                 StatIndexSharedData statIndexData = default;
//                 
//                 for (int i = 0; i < sortedStats.Count; i++)
//                 {
//                     var stat = sortedStats[i];
//                     var config = statsContainer.GetStat(stat.Id);
//
//                     if (config.ValueType == StatValueType.Float)
//                     {
//                         var value = stat.FloatValue.Value;
//                         //Base value index
//                         floatStats.List.Add(value);
//                         var baseIndex = (byte)floatStats.List.Length;
//                         //Add value
//                         floatStats.List.Add(0);
//                         //Mul value
//                         floatStats.List.Add(0);
//
//                         var indexing = new StatIndexTuple(config.Id, baseIndex);
//
//                         if (config.IsDynamic)
//                         {
//                             //Dynamic index
//                             floatStats.List.Add(value);
//                             indexing.DynamicIndex = (byte)floatStats.List.Length;
//                         }
//                         
//                         statIndexData.Values.AddNoResize(indexing);
//                     }
//                     
//                 }
//
//                 if (floatStats.List.Length > 0)
//                 {
//                     AddComponent(entity,floatStats);
//                 }
//
//                 if (sortedStats.Count > 0)
//                 {
//                     AddSharedComponent(entity,statIndexData);
//                 }
//             }
//
//         }
//     }
// }

