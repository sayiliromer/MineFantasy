using Unity.Entities;
using Unity.NetCode;

namespace Core
{
    public class EcsBootstrap : ClientServerBootstrap
    {
        public override bool Initialize(string defaultWorldName)
        {
            return false;
        }
        
        public new static World CreateClientWorld(string name)
        {
            return ClientServerBootstrap.CreateClientWorld(name);
        }

        public new static World CreateServerWorld(string name)
        {
            return ClientServerBootstrap.CreateServerWorld(name);
        }
    }
}