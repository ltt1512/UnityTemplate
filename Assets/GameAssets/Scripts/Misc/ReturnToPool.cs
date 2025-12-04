using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public class ReturnToPool : MonoBehaviour
    {
        public float lifeTime = 1.5f;
        public ObjectPool<GameObject> pool;

        void Awake()
        {
            var ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                lifeTime = main.duration + main.startLifetime.constantMax;
            }
        }

        void OnEnable()
        {
            Invoke(nameof(ReturnToPoolMethod), lifeTime);
        }

        void ReturnToPoolMethod()
        {
            if(pool != null)
                pool.Release(gameObject);
        }
    }
}