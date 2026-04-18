using System;
using UnityEngine;

namespace ZhengHua
{
    public class ProjectileObject_Poop : ProjectileObject
    {
        [SerializeField] private float liveTime = 3f;
        private float _timer = 0f;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > liveTime)
            {
                this.gameObject.SetActive(false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _timer = 0f;
        }
    }
}
