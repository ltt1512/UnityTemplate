using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public enum PoolType
    {
        AddCoin,
        AddExp
    }

    [Serializable]
    public class PoolObject
    {
        public GameObject prefab;
        public PoolType type;
        public bool isReturnToPool;
        public ObjectPool<GameObject> pool;

        public void Init()
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        }

        public GameObject Get()

        {
            return pool.Get();
        }

        private GameObject CreatePooledItem()
        {
            var ps = GameObject.Instantiate(prefab);
            if (isReturnToPool)
            {
                var returnToPool = ps.gameObject.GetComponent<ReturnToPool>();
                if (returnToPool == null)
                    returnToPool = ps.gameObject.AddComponent<ReturnToPool>();
                Debug.Log(pool);
                returnToPool.pool = pool;
            }
            return ps;
        }

        private void OnTakeFromPool(GameObject t)
        {
            t.SetActive(true);
        }

        private void OnReturnedToPool(GameObject t)
        {
            t.SetActive(false);
        }
        private void OnDestroyPoolObject(GameObject t)
        {
            GameObject.Destroy(t);
        }
    }

    public class PoolCtrl : BaseCtrl
    {

        public List<PoolObject> objPools;
        [ShowInInspector]
        public Dictionary<PoolType, PoolObject> objPoolDic = new();
        public override void Init()
        {

            objPoolDic.Clear();
            foreach (var pool in objPools)
            {
                pool.Init();
                if (!objPoolDic.ContainsKey(pool.type))
                    objPoolDic.Add(pool.type, pool);
            }
        }


        public GameObject GetObj(PoolType type)
        {
            if (objPoolDic.TryGetValue(type, out var pool))
                return pool.Get();
            return null;
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }
    }
}