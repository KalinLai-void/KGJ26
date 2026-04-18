using KalinKonta;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace KalinKonta.Stationery
{
    public enum DraggableState { Free, WaitForSelected };

    public class DraggableStationery : Stationery, 
        IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public float rotationSpeed = 0.05f;
        private bool isDragging = false;

        private StationeryHighlight highlighter;

        public DraggableState state;
        public bool canDragging = true;

        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            state = DraggableState.WaitForSelected;
        }

        protected override void Start()
        {
            base.Start();

            highlighter = GetComponent<StationeryHighlight>();
            if (highlighter == null) highlighter = gameObject.AddComponent<StationeryHighlight>();
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
            if (!canDragging) return;

            Vector2 scroll = InputManager.Instance.ScrollValue;

            if (Mathf.Abs(scroll.y) > 0.01f)
            {
                float rotationAmount = scroll.y * rotationSpeed;
                transform.Rotate(0, 0, rotationAmount);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!canDragging) return;
            if (isDragging) return;
            if (highlighter != null) highlighter.ToggleHighlight(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!canDragging) return;
            if (isDragging) return;
            if (highlighter != null) highlighter.ToggleHighlight(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canDragging) return;
            if (highlighter != null) highlighter.ToggleHighlight(false);

            isDragging = true;
            if (state == DraggableState.WaitForSelected) 
                StationerySpawner.Instance.SelectedObj(gameObject);
            Debug.Log($"Click on {gameObject.name}");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!canDragging) return;
            Vector3 screenPos = eventData.position;
            screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!canDragging) return;
            isDragging = false;
            if (state == DraggableState.WaitForSelected) 
                StationerySpawner.Instance.GenerateStationery(); // new round
            state = DraggableState.Free;
        }
    }
}