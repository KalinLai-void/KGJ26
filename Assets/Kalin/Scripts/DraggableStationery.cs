using UnityEngine;

public class DraggableStationery : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 zOffset;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        isDragging = true;
        zOffset = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 curMousePos = Input.mousePosition + zOffset;
        curMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curMousePos);
        transform.position = worldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
        // 如果是第一次從「按鈕位」拉下來，可以在這裡觸發一些邏輯
    }
}