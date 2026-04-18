using KalinKonta;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace KalinKonta.Stationery
{
    public class DraggableStationery : Stationery, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private float rotationSpeed = 0.05f;
        private bool isDragging = false;

        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        private void Update()
        {
            if (isDragging)
            {
                HandleScrollRotation();
            }
        }

        private void HandleScrollRotation()
        {
            Vector2 scroll = InputManager.Instance.ScrollValue;

            if (Mathf.Abs(scroll.y) > 0.01f)
            {
                float rotationAmount = scroll.y * rotationSpeed;
                transform.Rotate(0, 0, rotationAmount);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;
            Debug.Log($"Click on {gameObject.name}");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 screenPos = eventData.position;
            screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
        }
    }
}