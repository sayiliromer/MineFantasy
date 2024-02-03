using Mine.Core;
using TMPro;
using Unity.Entities;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

namespace Mine.Client.View
{
    public class TestUI : MonoBehaviour
    {
        public Button HostButton;
        public Button StopHostButton;
        public Button ConnectButton;
        public Button DisconnectButton;
        public TMP_InputField ConnectAddress;

        // Start is called before the first frame update
        void Start()
        {
            HostButton.onClick.AddListener(Host);
            StopHostButton.onClick.AddListener(StopHost);
            ConnectButton.onClick.AddListener(Connect);
            DisconnectButton.onClick.AddListener(Disconnect);
        }

        private void Update()
        {
            var serverWorld = EcsUtility.GetWorld(WorldFlags.GameServer);
            HostButton.gameObject.SetActive(serverWorld == null);
            StopHostButton.gameObject.SetActive(serverWorld != null);

            var clientWorld = EcsUtility.GetWorld(WorldFlags.GameClient);
            var connectionState = clientWorld.GetConnectionState();
            ConnectButton.gameObject.SetActive(connectionState == NetworkConnection.State.Disconnected);
            DisconnectButton.gameObject.SetActive(connectionState != NetworkConnection.State.Disconnected);
        }

        private void Connect()
        {
            var world = EcsUtility.GetWorld(WorldFlags.GameClient);
            if (world == null)
            {
                world = EcsBootstrap.CreateClientWorld("Client");
            }
            world.Connect(ConnectAddress.text, 22066);
        }

        private void Disconnect()
        {
            var world = EcsUtility.GetWorld(WorldFlags.GameClient);
            world.DestroyWorld();
        }

        private void Host()
        {
            var world = EcsBootstrap.CreateServerWorld("Server");
            world.Listen(22066);
        }

        private void StopHost()
        {
            var world = EcsUtility.GetWorld(WorldFlags.GameServer);
            world.DestroyWorld();
        }
    }
}
