using UnityEngine;
using ZhengHua.Common;

namespace ZhengHua
{
    public class ProjectilePool : ObjectPool<ProjectilePool>
    {
        [SerializeField] private ProjectileObject_Poop _poopPrefab;

        public static ProjectileObject_Poop GetPoop()
        {
            return Instance.GetObject(Instance._poopPrefab);
        }
    }
}