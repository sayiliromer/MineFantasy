using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Mine.Core
{
    public static class EcsUtility
    {
        public static World GetWorld(WorldFlags worldFlag)
        {
            foreach (var world in World.All)
            {
                if ((world.Flags & worldFlag) == worldFlag) return world;
            }

            return null;
        }

        public static World GetWorld(string name)
        {
            foreach (var world in World.All)
            {
                if (world.Name == name) return world;
            }

            return null;
        }
    }


    public class EcsBootstrap : ClientServerBootstrap
    {
        public override bool Initialize(string defaultWorldName)
        {
            
            return false;
        }


        //public static void Listen(int port)
        //{
        //    var serverWorld = GetWorld(WorldFlags.GameServer);
        //    Listen(serverWorld, port);
        //}

        //public static void Listen(World serverWorld, int port)
        //{
        //    serverWorld.EntityManager.CreateSingleton(new NetworkStreamRequestListen
        //    {
        //        Endpoint = NetworkEndpoint.Parse("0.0.0.0", (ushort)port)
        //    });
        //}

        //public static void Connect(World clientWorld, NetworkEndpoint endpoint)
        //{
        //    clientWorld.EntityManager.CreateSingleton(new NetworkStreamRequestConnect
        //    {
        //        Endpoint = endpoint
        //    }, "ConnectEntity");
        //}

        //public static void Connect(NetworkEndpoint endpoint)
        //{
        //    var clientWorld = GetWorld(WorldFlags.GameClient);
        //    Connect(clientWorld, endpoint);
        //}
        //public static void Connect(IPEndPoint endPoint)
        //{
        //    Connect(NetworkEndpoint.Parse(endPoint.Address.ToString(), (ushort)endPoint.Port));
        //}

        //public static void Connect(World clientWorld, IPEndPoint endPoint)
        //{
        //    Connect(clientWorld, NetworkEndpoint.Parse(endPoint.Address.ToString(), (ushort)endPoint.Port));
        //}


        //public static void Connect(World clientWorld, string address, int port)
        //{
        //    Connect(clientWorld, NetworkEndpoint.Parse(address, (ushort)port));
        //}


    }
}
