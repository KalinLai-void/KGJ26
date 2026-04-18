using KalinKonta;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace KalinKonta.Stationery
{
    public enum DraggableState { Free, WaitToSelected };

    public class DraggableStationery : Stationery, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [HideInInspector] public float rotationSpeed = 0.05f;
        private bool isDragging = false;

        private Vector3 originPos;
        private Quaternion originRot;

        [HideInInspector] public DraggableState state;

        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            state = DraggableState.WaitToSelected;
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
            originPos = gameObject.transform.position;
            originRot = gameObject.transform.rotation;
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

            if (state == DraggableState.WaitToSelected) // not selected yet
            {
                gameObject.transform.position = originPos;
                gameObject.transform.rotation = originRot;
            }
        }
    }
}