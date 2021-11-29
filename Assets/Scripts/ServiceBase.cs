using UnityEngine;

public abstract class ServiceBase<T> : MonoBehaviour
{
    protected virtual void Awake()
    {
        Bind();
    }

    protected virtual void OnDestroy()
    {
        Unbind();
    }

    protected virtual void Bind()
    {
        
    }

    protected virtual void Unbind()
    {
    }
}
