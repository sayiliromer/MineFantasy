using Mine.Core;
using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public abstract class StatBaker<T> : Baker<T> where T : Component
    {
        protected DynamicBuffer<FloatDynamicStat> FloatDynamicBuffer;
        protected DynamicBuffer<FloatStaticStat> FloatStaticBuffer;

        public sealed override void Bake(T authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            FloatStaticBuffer = AddBuffer<FloatStaticStat>(entity);
            FloatDynamicBuffer = AddBuffer<FloatDynamicStat>(entity);
            OnBake(authoring);
        }

        protected abstract void OnBake(T authoring);

        protected void AddFloatStaticStat<TMarker>(float amount) where TMarker : unmanaged, IStaticStatMarker
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var index = FloatStaticBuffer.Add(new FloatStaticStat(amount));
            var marker = index.ReinterpretCast<TMarker>();
            AddComponent(entity,marker);
        }

        protected void AddFloatDynamicStat<TMarker>(float amount) where TMarker : unmanaged, IDynamicStatMarker
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var staticStatIndex = FloatStaticBuffer.Add(new FloatStaticStat(amount));
            var dynamicStatIndex = FloatDynamicBuffer.Add(new FloatDynamicStat(amount));
            var dummy = new DynamicStatMarkerDummy
            {
                StaticIndex = staticStatIndex,
                DynamicIndex = dynamicStatIndex,
            };
            var marker = dummy.ReinterpretCast<DynamicStatMarkerDummy, TMarker>();
            AddComponent(entity,marker);
        }

        private struct DynamicStatMarkerDummy
        {
            public int StaticIndex;
            public int DynamicIndex;
        }
    }
}