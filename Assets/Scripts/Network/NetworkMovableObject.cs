using System;
using UnityEngine;
using UnityEngine.Networking;


namespace MaksK_SpaceGB
{
#pragma warning disable 618
    public abstract class NetworkMovableObject : NetworkBehaviour
#pragma warning restore 618
    {
        protected abstract float _speed { get; }

#pragma warning disable 618
        [SyncVar] protected Vector3 _serverPosition;
        [SyncVar] protected Vector3 _serverEuler;
#pragma warning restore 618

        protected virtual void Movement()
        {
            if (hasAuthority)
            {
                HasAuthorityMovement();
            }
            else
            {
                FromServerUpdate();
            }
        }

        protected abstract void HasAuthorityMovement();

        protected abstract void FromServerUpdate();

        protected abstract void SendToServer();
    }
}



