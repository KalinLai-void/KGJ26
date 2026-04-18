using System.Collections.Generic;
using UnityEngine;

namespace ZhengHua.Common
{
    public abstract class ObjectPool<T> : Singleton<T> where T : ObjectPool<T>
    {
        private IDictionary<string, IList<GameObject>> _pool = new Dictionary<string, IList<GameObject>>();

        protected TK GetObject<TK>(TK prefab) where TK : MonoBehaviour
        {
            if (!_pool.TryGetValue(typeof(TK).Name, out var pool))
            {
                pool = new List<GameObject>();
                _pool.Add(typeof(TK).Name, pool);
            }

            GameObject go = null;
            foreach (var poolObject in pool)
            {
                if (!poolObject.activeInHierarchy)
                {
                    go = poolObject;
                    break;
                }
            }

            if (go is null)
            {
                go = Instantiate(prefab).gameObject;
                go.name = $"{typeof(TK).Name} ({pool.Count})";
                go.transform.SetParent(this.transform);
                pool.Add(go.gameObject);
            }

            return go.GetComponent<TK>();
        }
    }
}