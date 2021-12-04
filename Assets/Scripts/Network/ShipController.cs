﻿using UnityEngine;
using UnityEngine.Networking;


namespace MaksK_SpaceGB
{
    public class ShipController : NetworkMovableObject
    {
        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        protected override float _speed => _shipSpeed;

        [SerializeField] private Transform _cameraAttach;
        private CameraOrbit _cameraOrbit;
        private PlayerLabel _playerLabel;
        private float _shipSpeed;
        private Rigidbody _rigidBody;

        [SyncVar] private string _playerName;

        private void OnCollisionEnter(Collision collision)
        {
            CmdDoDamage();
        }

        private void OnGUI()
        {
            if (_cameraOrbit == null)
            {
                return;
            }
            _cameraOrbit.ShowPlayerLabels(_playerLabel);
        }

        [Command]
        public void CmdChangeName(string playerName)
        {
            _playerName = playerName;
            gameObject.name = playerName;
            Debug.Log($"Command change on server {playerName}");
            RpcChangeName(_playerName);
        }

        [ClientRpc]
        public void RpcChangeName(string playerName)
        {
            gameObject.name = playerName;
            //_playerName = playerName;
            Debug.Log($"Command change on client {playerName}");
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            gameObject.name = _playerName;
            Debug.Log($"On client start {_playerName}");
        }

        public override void OnStartAuthority()
        {
            //Подгрузить имя игрока из другого класса
            //Передать имя на сервер
            //изменить имя в других клиентах
            _playerName = "Qwerty";
            CmdChangeName(_playerName);

            //Пробежаться по списку кораблей 

            var objects = ClientScene.objects;

            //for (int i = 0; i < objects.Count; i++)
            //{
            //    var obj = objects.ElementAt(i).Value;                

            //    var ship = obj.GetComponent<ShipController>();

            //    if (ship != null && obj.transform != transform)
            //    {
            //        obj.gameObject.name = ship.PlayerName;
            //        Debug.Log(ship.PlayerName);
            //    }
            //}

            _rigidBody = GetComponent<Rigidbody>();

            if (_rigidBody == null)
            {
                return;
            }

            //gameObject.name = _playerName;
            _cameraOrbit = Camera.main.GetComponent<CameraOrbit>();
            _cameraOrbit.Initiate(_cameraAttach == null ? transform : _cameraAttach);
            _playerLabel = GetComponentInChildren<PlayerLabel>();

            base.OnStartAuthority();
        }

        private void Update()
        {
            Movement();
        }

        [ClientCallback]
        private void LateUpdate()
        {
            _cameraOrbit?.CameraMovement();
        }        

        [Command]
        private void CmdDoDamage()
        {
            SolarSystemNetworkManager.DestroyObject(gameObject);
        }

        protected override void HasAuthorityMovement()
        {
            var spaceShipSettings = SettingsContainer.Instance?.SpaceShipSettings;

            if (spaceShipSettings == null)
            {
                return;
            }

            var isFaster = Input.GetKey(KeyCode.LeftShift);
            var speed = spaceShipSettings.ShipSpeed;
            var faster = isFaster ? spaceShipSettings.Faster : 1.0f;

            _shipSpeed = Mathf.Lerp(_shipSpeed, speed * faster,
                SettingsContainer.Instance.SpaceShipSettings.Acceleration);

            var currentFov = isFaster
                ? SettingsContainer.Instance.SpaceShipSettings.FasterFov
                : SettingsContainer.Instance.SpaceShipSettings.NormalFov;
            _cameraOrbit.SetFov(currentFov, SettingsContainer.Instance.SpaceShipSettings.ChangeFovSpeed);

            var velocity = _cameraOrbit.transform.TransformDirection(Vector3.forward) * _shipSpeed;
            _rigidBody.velocity = velocity * Time.deltaTime;

            //if (!Input.GetKey(KeyCode.C))
            //{
            //    var targetRotation = Quaternion.LookRotation(
            //        Quaternion.AngleAxis(_cameraOrbit.LookAngle, -transform.right) *
            //        velocity);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            //}
        }

        protected override void FromServerUpdate() { }
        protected override void SendToServer() { }
    }
}
