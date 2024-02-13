using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    [RequireComponent(typeof(StatAuthoring))]
    public class RefillStatAuthoring : MonoBehaviour
    {
        [Serializable]
        public class RefillTuple
        {
            [ValueDropdown("GetOptions")]
            public byte ToFillStatId;
            [ValueDropdown("GetOptions")]
            public byte AmountStatId;
            
            [NonSerialized] public RefillStatAuthoring Authoring;

            private IEnumerable GetOptions()
            {
                if (Authoring == null) return null;
                return Authoring.GetOptions();
            }
        }

        public StatAuthoring StatAuthoring;
        [OnInspectorInit("SetAuthoring")]
        public List<RefillTuple> Tuples;

        void SetAuthoring()
        {
            foreach (var tuple in Tuples)
            {
                tuple.Authoring = this;
            }
        }
        
        private void Reset()
        {
            StatAuthoring = GetComponent<StatAuthoring>();
        }

        public class RefillStatAuthoringBaker : Baker<RefillStatAuthoring>
        {
            public override void Bake(RefillStatAuthoring authoring)
            {
                DependsOn(authoring.StatAuthoring);
                if(authoring.StatAuthoring == null) return;
                var statsConfig = StatConfigContainer.GetInstance();
                if(statsConfig == null) return;
                if(authoring.Tuples == null || authoring.Tuples.Count == 0) return;
                var entity = GetEntity(TransformUsageFlags.None);
                RefillIndexListData listData = default;
                for (int i = 0; i < authoring.Tuples.Count; i++)
                {
                    var refill = authoring.Tuples[i];
                    listData.List.Add(new IndexTuple()
                    {
                        AmountIndex = authoring.StatAuthoring.GetBaseStatValueIndex(refill.AmountStatId),
                        ToRefillIndex = authoring.StatAuthoring.GetDynamicStatValueIndex(refill.ToFillStatId)
                    });
                }

                if (listData.List.Length > 0)
                {
                    AddComponent(entity, listData);
                }
            }
        }

        public IEnumerable GetOptions()
        {
            var sortedStats = StatAuthoring.GetSortedStats();
            var vd = new ValueDropdownList<byte>();

            for (int i = 0; i < sortedStats.Count; i++)
            {
                var stat = sortedStats[i];
                vd.Add(stat.Type, stat.Id);
            }
            
            return vd;
        }

        public List<byte> GetStats()
        {
            if (StatAuthoring == null || StatAuthoring.Stats.Count == 0)
            {
                return null;
            }
            var sortedStats = StatAuthoring.GetSortedStats();
            return sortedStats.Select(s => (byte)s.Id).ToList();
        }
    }
}