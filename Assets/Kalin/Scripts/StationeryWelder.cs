using KalinKonta.Stationery;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZhengHua;

namespace KalinKonta.Stationery
{
    public class StationeryWelder : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorLayer;

        private bool isWeldingPerformed = false;

        private void OnEnable()
        {
            GameManager.OnStage2Start?.AddListener(ExecuteWeld);
            GameManager.OnStage1Start?.AddListener(EnterStage1);
        }

        private void OnDestroy()
        {
            GameManager.OnStage2Start?.RemoveListener(ExecuteWeld);
        }

        private void EnterStage1()
        {
            isWeldingPerformed = false;

            if (StationerySpawner.Instance != null)
            {
                StationerySpawner.Instance.enabled = true;
            }

            WeldedGroupHealthProxy[] weldedGroups = FindObjectsByType<WeldedGroupHealthProxy>(FindObjectsSortMode.None);

            foreach (var group in weldedGroups)
            {
                Transform groupTransform = group.transform;

                List<Transform> children = new List<Transform>();
                for (int i = 0; i < groupTransform.childCount; i++)
                {
                    children.Add(groupTransform.GetChild(i));
                }

                foreach (var child in children)
                {
                    RestoreStationery(child.gameObject);
                }

                Destroy(group.gameObject);
            }

            DraggableStationery[] allItems = FindObjectsByType<DraggableStationery>(FindObjectsSortMode.None);
            foreach (var item in allItems)
            {
                if (item.transform.parent == null || item.transform.parent.GetComponent<WeldedGroupHealthProxy>() == null)
                {
                    RestoreStationery(item.gameObject);
                }
            }
        }

        public void ExecuteWeld()
        {
            if (isWeldingPerformed) return;
            isWeldingPerformed = true;

            StationerySpawner.Instance.enabled = false;

            DraggableStationery[] allItems = FindObjectsByType<DraggableStationery>(FindObjectsSortMode.None);

            HashSet<GameObject> processed = new HashSet<GameObject>(); // Avoid repeats

            foreach (var itemA in allItems)
            {
                if (processed.Contains(itemA.gameObject)) continue;
                itemA.GetComponent<Rigidbody>().isKinematic = false;

                List<GameObject> neighbors = FindTouchingItems(itemA.gameObject, allItems);

                bool shouldBeKinematic = false;

                if (neighbors.Count > 0)
                {
                    // Creating a new root (scale must Vector3.one)
                    GameObject groupRoot = new GameObject($"WeldedGroup_{itemA.name}");
                    groupRoot.transform.position = itemA.transform.position;
                    groupRoot.transform.rotation = itemA.transform.rotation;
                    groupRoot.transform.localScale = Vector3.one;
                    groupRoot.layer = itemA.gameObject.layer;
                    groupRoot.transform.SetParent(itemA.transform.parent);

                    groupRoot.AddComponent<WeldedGroupHealthProxy>();

                    Rigidbody rootRb = groupRoot.AddComponent<Rigidbody>();
                    rootRb.linearDamping = 2f;
                    rootRb.angularDamping = 2f;
                    if(itemA.TryGetComponent<Rigidbody>(out var itemARb))
                    {
                        rootRb.constraints = itemARb.constraints;
                    }

                    neighbors.Add(itemA.gameObject);

                    foreach (var member in neighbors)
                    {
                        if (processed.Contains(member)) continue;

                        if (CheckIsTouchingFloor(member))
                        {
                            shouldBeKinematic = true;
                        }

                        if (member.TryGetComponent(out Rigidbody mRb)) Destroy(mRb);
                        if (member.TryGetComponent(out DraggableStationery ds))
                        {
                            ds.canDragging = false;
                        }

                        member.transform.SetParent(groupRoot.transform, true);
                        processed.Add(member);
                    }

                    if (shouldBeKinematic)
                    {
                        rootRb.isKinematic = true;
                    }
                    else
                    {
                        if (itemA.TryGetComponent<Rigidbody>(out var itemARbII))
                        {
                            rootRb.constraints = itemARbII.constraints;
                        }
                    }

                    rootRb.ResetCenterOfMass();
                }
                else
                {
                    if (itemA.TryGetComponent(out DraggableStationery ds))
                    {
                        ds.canDragging = false;
                    }

                    if (itemA.TryGetComponent(out Rigidbody rb))
                    {
                        rb.isKinematic = CheckIsTouchingFloor(itemA.gameObject);
                    }
                }
            }
        }

        private void RestoreStationery(GameObject obj)
        {
            obj.transform.SetParent(null);

            if (obj.TryGetComponent(out DraggableStationery ds))
            {
                ds.canDragging = true;
                ds.state = DraggableState.Free;
            }

            if (!obj.GetComponent<Rigidbody>())
            {
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.linearDamping = 2f;
                rb.angularDamping = 2f;
            }
            else
            {
                obj.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        private bool CheckIsTouchingFloor(GameObject obj)
        {
            Collider col = obj.GetComponent<Collider>();
            return Physics.CheckBox(col.bounds.center, col.bounds.extents, obj.transform.rotation, _floorLayer);
        }

        private List<GameObject> FindTouchingItems(GameObject root, DraggableStationery[] allItems)
        {
            List<GameObject> touches = new List<GameObject>();
            Collider rootCollider = root.GetComponent<Collider>();

            foreach (var other in allItems)
            {
                if (other.gameObject == root) continue;

                Collider otherCollider = other.GetComponent<Collider>();

                if (rootCollider.bounds.Intersects(otherCollider.bounds)) // Check if collider overlapping
                {
                    touches.Add(other.gameObject);
                }
            }
            return touches;
        }
    }
}