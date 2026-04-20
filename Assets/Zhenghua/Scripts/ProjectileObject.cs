using Nori;
using KalinKonta;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using KalinKonta.Stationery;

namespace ZhengHua
{
    
    public abstract class ProjectileObject : MonoBehaviour, 
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected float _maxSpeed = 10f;
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected bool _flyingUseGravity = false;

        [SerializeField] private AudioLibrary audioLibrary;

        private Highlighter highlighter;

        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            var outline = gameObject.AddComponent<Outline>();
            outline.enabled = false;
            highlighter = GetComponent<Highlighter>();
            if (highlighter == null) highlighter = gameObject.AddComponent<Highlighter>();
        }

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
            if(!_flyingUseGravity)
                _rigidbody.useGravity = false;
        }

        protected void OnCollisionEnter(Collision other)
        {
            if (!_flyingUseGravity && _rigidbody.useGravity == false)
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
                audioLibrary?.PlaySfx(SfxId.ClickPoop);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (highlighter != null) highlighter.ToggleHighlight(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (highlighter != null) highlighter.ToggleHighlight(false);
        }
    }
}
