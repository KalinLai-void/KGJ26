using KalinKonta;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KalinKonta.Stationery
{
    public class DraggableStationery : Stationery, IPointerDownHandler, IDragHandler
    {
        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"Click on {gameObject.name}");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 screenPos = eventData.position;
            screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }
    }
}