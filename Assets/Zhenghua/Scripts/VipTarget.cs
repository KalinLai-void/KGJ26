using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{
    public class VipTarget : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private GameObject hpContainer;
        [SerializeField] private Image[] hpImages;

        private int maxHp = 3;
        private int _nowHp = 3;

        private void Start()
        {
            _rigidbody.isKinematic = true;
            
            GameManager.OnStage2Start?.AddListener(OnGameStart);
            GameManager.OnStage2Finish?.AddListener(OnStageEnd);

            _nowHp = maxHp;
            for (int i = 0; i < hpImages.Length; i++)
            {
                hpImages[i].color = i < _nowHp ? Color.red : Color.gray;
            }
            
            hpContainer.SetActive(false);
        }

        private void OnGameStart()
        {
            _rigidbody.isKinematic = false;
            
            hpContainer.SetActive(true);
        }
        
        private void OnStageEnd()
        {
            _rigidbody.isKinematic = true;
            
            hpContainer.SetActive(false);
        }

        private void LoseAction()
        {
            _rigidbody.isKinematic = true;
            GameManager.OnGameEnd?.Invoke(false);
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
                _nowHp--;
                for (int i = 0; i < hpImages.Length; i++)
                {
                    hpImages[i].color = i < _nowHp ? Color.red : Color.gray;
                }
                if(_nowHp <= 0)
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