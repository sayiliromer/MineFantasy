using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public struct GhostRepo : IComponentData
    {
        public Entity NetworkedCube;
        public Entity PlayerUnit;
    }

    public class GhostRepoAuthoring : MonoBehaviour
    {
        public GameObject NetworkedCube;
        public GameObject PlayerUnit;

        public class Baker : Baker<GhostRepoAuthoring>
        {
            public override void Bake(GhostRepoAuthoring authoring)
            {
                var memberFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly;
                var members = authoring.GetType().GetFields(memberFlags);
                object result = new GhostRepo();
                var repoType = result.GetType();
                var mainEntity = GetEntity(TransformUsageFlags.None);
                foreach (var member in members)
                {
                    var value = member.GetValue(authoring);
                    if (value == null) continue;
                    Entity entity = Entity.Null;
                    if (value is GameObject gameObject)
                    {
                        entity = GetEntity(gameObject, TransformUsageFlags.None);
                    }
                    else if (value is Component component)
                    {
                        entity = GetEntity(component, TransformUsageFlags.None);
                    }

                    var targetMember = repoType.GetField(member.Name, memberFlags);
                    if (entity != Entity.Null && targetMember != null)
                    {
                        targetMember.SetValue(result, entity);
                    }
                }

                AddComponent(mainEntity, (GhostRepo)result);
            }
        }
    }
}
