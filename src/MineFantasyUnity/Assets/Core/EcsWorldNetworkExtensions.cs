using System.Net;
using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;

namespace Core
{
    public static class EcsWorldNetworkExtensions
    {
        public static void Connect(this World world, string endPoint, int port)
        {
            world.EntityManager.CreateSingleton(new NetworkStreamRequestConnect
            {
                Endpoint = NetworkEndpoint.Parse(endPoint, (ushort)port)
            });
        }

        public static NetworkConnection.State GetConnectionState(this World world)
        {
            if (world == null) return NetworkConnection.State.Disconnected;
            var connection = world.GetSingleton<NetworkStreamConnection>();
            if (connection == null) return NetworkConnection.State.Disconnected;
            var driver = world.GetSingleton<NetworkStreamDriver>();
            if (driver == null) return NetworkConnection.State.Disconnected;
            return driver.Value.GetConnectionState(connection.Value);
        }

        public static void Connect(this World world, IPEndPoint endPoint)
        {
            world.EntityManager.CreateSingleton(new NetworkStreamRequestConnect
            {
                Endpoint = NetworkEndpoint.Parse(endPoint.Address.ToString(), (ushort)endPoint.Port)
            });
        }

        public static void Listen(this World world, int port)
        {
            world.EntityManager.CreateSingleton(new NetworkStreamRequestListen
            {
                Endpoint = NetworkEndpoint.Parse("0.0.0.0", (ushort)port)
            });
        }

        public static void Disconnect(this World world)
        {
            var connectionEntity = world.GetSingletonEntity<NetworkStreamConnection>();
            world.EntityManager.AddComponentData(connectionEntity, new NetworkStreamRequestDisconnect
            {
                Reason = NetworkStreamDisconnectReason.ConnectionClose
            });
        }
    }
}