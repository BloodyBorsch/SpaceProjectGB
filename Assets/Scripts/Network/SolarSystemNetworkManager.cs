using UnityEngine;
using UnityEngine.Networking;


namespace MaksK_SpaceGB
{
    public class SolarSystemNetworkManager : NetworkManager
    {
        private string _playerName;
        private PopUpMenu _popUpMenu;

        private void Start()
        {
            _popUpMenu = GetComponentInChildren<PopUpMenu>(true);
            _popUpMenu.Initialize();
            _popUpMenu.ShowMenu();
        }

        private void Update()
        {
            if (_popUpMenu.isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
            {
                TypeName();
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            var spawnTransform = GetStartPosition();

            var player = Instantiate(playerPrefab, spawnTransform.position, spawnTransform.rotation);
            player.GetComponent<ShipController>().PlayerName = _playerName;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        public void GetName(string name)
        {
            _playerName = name;
        }

        private void TypeName()
        {
            _playerName = _popUpMenu.TypeName();
            _popUpMenu.CloseMenu();
        }
    }
}