using UnityEngine;
using UnityEngine.Networking;


namespace MaksK_SpaceGB
{
    public class SolarSystemNetworkManager : NetworkManager
    {
        private ShipData _shipData;
        private PopUpMenu _popUpMenu;

        private void Start()
        {
            _shipData = ResourceLoadHelper.Loader(UnitSwitcher.AverageSpaceShip);
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
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        private void TypeName()
        {
            _shipData.ShipName = _popUpMenu.TypeName();
            _popUpMenu.CloseMenu();
        }
    }
}