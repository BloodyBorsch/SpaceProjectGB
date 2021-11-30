using UnityEngine;


namespace MaksK_SpaceGB
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        public CameraOrbit CameraOrbit { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void BindCameraOrbit(CameraOrbit orbit)
        {
            CameraOrbit = orbit;
            Debug.Log($"Bind camera {orbit}");
        }

        public void UnbindCameraOrbit()
        {
            Debug.Log($"Unbind camera");
            CameraOrbit = null;
        }
    }
}
