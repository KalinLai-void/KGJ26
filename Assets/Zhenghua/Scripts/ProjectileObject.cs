using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZhengHua
{
    
    public abstract class ProjectileObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected float _maxSpeed = 10f;
        [SerializeField] protected Rigidbody _rigidbody;
        
        public Rigidbody Rigidbody => _rigidbody;

        protected virtual void FixedUpdate()
        {
            if(_rigidbody.linearVelocity.magnitude > _maxSpeed) _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxSpeed;
        }

        protected virtual void Reset()
        {
            if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void OnEnable()
        {
            _rigidbody.useGravity = false;
        }

        protected void OnCollisionEnter(Collision other)
        {
            if (_rigidbody.useGravity == false)
                _rigidbody.useGravity = true;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("DeadZone"))
            {
                _rigidbody.linearVelocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
            
            if (_rigidbody.useGravity == false)
                _rigidbody.useGravity = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                GameManager.OnClickedPoop?.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}
