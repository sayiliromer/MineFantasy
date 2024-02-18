using Unity.Entities;
 using UnityEngine;
 
 namespace Mine.ClientServer
 {
     [RequireComponent(typeof(BaseActiveAbilityItemAuthoring))]
     public class MeleeWeaponAbilityAuthoring : MonoBehaviour
     {
         public BaseActiveAbilityItemAuthoring BaseAbility;
         
         private void Reset()
         {
             BaseAbility = GetComponent<BaseActiveAbilityItemAuthoring>();
         }
 
         public class Baker : Baker<MeleeWeaponAbilityAuthoring>
         {
             public override void Bake(MeleeWeaponAbilityAuthoring authoring)
             {
                 DependsOn(authoring.BaseAbility);
                 if(!authoring.BaseAbility) return;
                 var entity = GetEntity(TransformUsageFlags.None);
                 //AddComponent<MeleeWeapon>(entity);
             }
         }
     }
 }