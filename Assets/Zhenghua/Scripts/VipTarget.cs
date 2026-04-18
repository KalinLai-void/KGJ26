using System;
using UnityEngine;

namespace ZhengHua
{
    public class VipTarget : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody.isKinematic = true;
            
            GameManager.OnStage2Start?.AddListener(OnGameStart);
        }

        private void OnGameStart()
        {
            _rigidbody.isKinematic = false;
        }

        private void LoseAction()
        {
            _rigidbody.isKinematic = true;
            GameManager.OnStage2Finish?.Invoke(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("DeadZone"))
            {
                LoseAction();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Poop"))
            {
                LoseAction();
            }
        }


        private void Reset()
        {
            if(_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }
    }
}