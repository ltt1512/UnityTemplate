using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class BaseBooster : MonoBehaviour
{
    public BoosterData Data;
    private bool _isActive;
   
    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;

        Use();
    }

    public virtual void Use()
    {

    }

    public virtual void Done()
    {
        _isActive = false;
    }

    public bool IsActive => _isActive;
}
