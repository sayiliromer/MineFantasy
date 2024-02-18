using Unity.Entities;

namespace Mine.Server
{
    
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    public partial struct BenchmarkSystem : ISystem
    {
        
        public void OnCreate(ref SystemState state)
        {
        }
        
        
        public void OnUpdate(ref SystemState state)
        {
        }
    }
}