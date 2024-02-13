using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mine.ClientServer
{
    [Serializable]
    public class StatConfig
    {
        public const byte DynamicParameterCount = 4;
        public const byte StaticParameterCount = 3;
        public byte Id;
        public string Name;
        public StatValueType ValueType;
        public bool IsDynamic;
    }

    [CreateAssetMenu(menuName = "Toos/Data/StatConfigConainer")]
    public class StatConfigContainer : ScriptableObject
    {
        public List<StatConfig> Stats;

        public static StatConfigContainer GetInstance()
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadAssetAtPath<StatConfigContainer>("Assets/Game/ClientServer/Prefabs/DataContainers/StatConfigContainer.asset");
#else
            return null;
#endif
        }
        public StatConfig GetStat(int id)
        {
            return Stats.Find(s => s.Id == id);
        }
        
        public StatConfig GetStat(string name)
        {
            return Stats.Find(s => s.Name == name);
        }
    }
}