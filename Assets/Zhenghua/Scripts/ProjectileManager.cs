using System;
using System.Collections;
using UnityEngine;
using ZhengHua.Common;
using ZhengHua.ScriptableObjects;
using Random = UnityEngine.Random;

namespace ZhengHua
{
    public class ProjectileManager : Singleton<ProjectileManager>
    {
        [SerializeField] private Transform _vipTransform;
        public void ShootPoop(float force, float progress = -1f)
        {
            var poop = ProjectilePool.GetPoop();
            poop.transform.position = GetSemiCirclePosition(progress, _radius);
            var direction = (_vipTransform.position - poop.transform.position).normalized;
            poop.transform.forward = direction + Vector3.one * Random.Range(0.5f, 3f);
            poop.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
            poop.Rigidbody.AddTorque(Random.insideUnitSphere * _torqueForce, ForceMode.Impulse);
        }
        
        [SerializeField] private float _force;
        [SerializeField] private float _torqueForce;
        [SerializeField] private float _radius;
        private LevelData _levelData;
        private int _levelIndex = 0;
        private float _gameTime = 0f;
        private bool _isGameStart = false;
        private bool _inLevel = false;
        private bool _isLevelEnd = false;

        private void Start()
        {
            GameManager.OnStage2Start?.AddListener(OnGameStart);
            GameManager.OnStage2Finish?.AddListener(OnStageEnd);
        }

        private void OnGameStart()
        {
            _levelIndex = 0;
            _gameTime = 0f;
            _isGameStart = true;
            _inLevel = false;
            _isLevelEnd = false;
            _levelData = GameManager.CurrentLevel;
        }

        private void OnStageEnd()
        {
            _isGameStart = false;
            CancelInvoke(nameof(Stage2End));
        }

        private void Update()
        {
            if (_isGameStart == false)
                return;
            
            if(_isGameStart)
                _gameTime += Time.deltaTime;

            if (!_inLevel && !_isLevelEnd && _gameTime > _levelData.levelDataItems[_levelIndex].startTime)
            {
                _inLevel = true;
                EnterLevel();
            }
        }

        private void EnterLevel()
        {
            StartCoroutine(_StartCoroutine());

            IEnumerator _StartCoroutine()
            {
                for (int i = 0; i < _levelData.levelDataItems[_levelIndex].shootCount; i++)
                {
                    ShootPoop(_force, _levelData.levelDataItems[_levelIndex].progress);
                    yield return new WaitForSeconds(_levelData.levelDataItems[_levelIndex].everyDelay);
                }

                _inLevel = false;
                _levelIndex++;
                if (_levelIndex > _levelData.levelDataItems.Length - 1)
                {
                    _inLevel = true;
                    _isLevelEnd = true;

                    if (_isLevelEnd)
                    {
                        Invoke(nameof(Stage2End), 5f);
                    }
                }
            }
        }

        private void Stage2End()
        {
            GameManager.OnStage2Finish?.Invoke();
        }

        private Vector3 GetSemiCirclePosition(float progress, float radius)
        {
            progress = Mathf.Clamp(progress, 0f, 1f);
        
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
            ShootPoop(_force);
        }
    }
}