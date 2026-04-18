using UnityEngine;
using ZhengHua.Common;

namespace ZhengHua
{
    public class ProjectileManager : Singleton<ProjectileManager>
    {
        [SerializeField] private Transform _vipTransform;
        public void ShootPoop(Vector3 startPosition, Vector3 direction, float force)
        {
            var poop = ProjectilePool.GetPoop();
            poop.transform.position = GetSemiCirclePosition(_radius);
            direction = (_vipTransform.position - poop.transform.position).normalized;
            poop.transform.forward = direction + Vector3.one * Random.Range(0.5f, 3f);
            poop.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
            poop.Rigidbody.AddTorque(Random.insideUnitSphere * _torqueForce, ForceMode.Impulse);
        }

        [SerializeField] private Transform _startPosition;
        [SerializeField] private Vector3 _shootDirection;
        [SerializeField] private float _force;
        [SerializeField] private float _torqueForce;
        [SerializeField] private float _radius;
        
        private Vector3 GetSemiCirclePosition(float radius)
        {
            // 將索引轉換為 0 到 1 之間的比例
            float progress = Random.Range(0.2f, 0.8f);
        
            float angleRange = 180f;
        
            // 計算當前角度（從左邊 -90度 到 右邊 90度）
            float currentAngle = (progress * angleRange) - 90f;
        
            // 轉換為弧度
            float radian = currentAngle * Mathf.Deg2Rad;

            // 計算局部座標 (Local Space)
            // x 控制左右偏移 (Sin)，y 控制上下偏移 (Cos)
            float x = Mathf.Sin(radian) * radius;
            float y = Mathf.Cos(radian) * radius;

            // 轉換為世界座標 (考慮物件的旋轉與位置)
            // 使用 transform.right 是為了對應物件的左側/右側
            // 使用 transform.up 是為了向上拱起
            return transform.position + (transform.right * x) + (transform.up * y);
        }

        public void ShootPoop()
        {
            ShootPoop(_startPosition.position, _shootDirection, _force);
        }
    }
}