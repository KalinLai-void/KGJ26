using UnityEngine;
using ZhengHua.Common;

namespace ZhengHua
{
    public class ProjectileManager : Singleton<ProjectileManager>
    {
        public void ShootPoop(Vector3 startPosition, Vector3 direction, float force)
        {
            var poop = ProjectilePool.GetPoop();
            poop.transform.position = startPosition;
            poop.transform.forward = direction;
            poop.Rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
            poop.Rigidbody.AddTorque(Random.insideUnitSphere * _torqueForce, ForceMode.Impulse);
        }

        [SerializeField] private Transform _startPosition;
        [SerializeField] private Vector3 _shootDirection;
        [SerializeField] private float _force;
        [SerializeField] private float _torqueForce;

        public void ShootPoop()
        {
            ShootPoop(_startPosition.position, _shootDirection, _force);
        }
    }
}