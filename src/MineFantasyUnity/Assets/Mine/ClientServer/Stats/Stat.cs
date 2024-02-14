using System;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct FloatStatList : IComponentData
    {
        [GhostField] public FixedList32Bytes<float> List;

        // public FloatStat(float value)
        // {
        //     Value = value;
        // }
        //
        // public static implicit operator FloatStat(float value)
        // {
        //     return new FloatStat(value);
        // }
    }

    [InternalBufferCapacity(10)]
    public struct FloatStaticStat : IBufferElementData
    {
        [GhostField] public float BaseValue, Add, Mul;
        public float FinalValue => (BaseValue + Add) * (1f + Mul);

        public FloatStaticStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }

    [InternalBufferCapacity(10)]
    public struct FloatDynamicStat : IBufferElementData
    {
        [GhostField] public float Value;

        public FloatDynamicStat(float value)
        {
            Value = value;
        }
    }

    [InternalBufferCapacity(10)]
    public struct IntStaticStat : IBufferElementData
    {
        [GhostField] public int BaseValue, Add;
        [GhostField] public float Mul;
        public int FinalValue => (int)((BaseValue + Add) * (1f + Mul));

        public IntStaticStat(int baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }

    [InternalBufferCapacity(10)]
    public struct IntDynamicStat : IBufferElementData
    {
        [GhostField] public int Value;

        public IntDynamicStat(int value)
        {
            Value = value;
        }
    }


    public enum StatValueType
    {
        Float,
        Int
    }

    [Serializable]
    public struct StatIndexTuple
    {
        public byte Id;
        public byte Index;
        public byte DynamicIndex;

        public StatIndexTuple(byte id, byte index)
        {
            Id = id;
            Index = index;
            //Means it is not a dynamic stat
            DynamicIndex = 255;
        }
    }

    public struct StatIndexSharedData : ISharedComponentData
    {
        public FixedList32Bytes<StatIndexTuple> Values;
    }
}