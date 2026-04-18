using KalinKonta.Stationery;
using System.Collections.Generic;
using UnityEngine;
using ZhengHua;

namespace KalinKonta.Stationery
{
    public class StationeryWelder : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorLayer;

        private void OnEnable()
        {
            GameManager.OnStage2Start?.AddListener(ExecuteWeld);
        }

        private void OnDisable()
        {
            GameManager.OnStage2Start?.RemoveListener(ExecuteWeld);
        }

        public void ExecuteWeld()
        {
            StationerySpawner.Instance.enabled = false;

            DraggableStationery[] allItems = FindObjectsByType<DraggableStationery>(FindObjectsSortMode.None);

            HashSet<GameObject> processed = new HashSet<GameObject>(); // Avoid repeats

            foreach (var itemA in allItems)
            {
                if (processed.Contains(itemA.gameObject)) continue;

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

                    Rigidbody rootRb = groupRoot.AddComponent<Rigidbody>();
                    rootRb.linearDamping = 2f;
                    rootRb.angularDamping = 2f;
                    rootRb.constraints = itemA.GetComponent<Rigidbody>().constraints;

                    neighbors.Add(itemA.gameObject);

                    foreach (var member in neighbors)
                    {
                        if (processed.Contains(member)) continue;

                        if (CheckIsTouchingFloor(member))
                        {
                            shouldBeKinematic = true;
                        }

                        if (member.TryGetComponent(out Rigidbody mRb)) Destroy(mRb);
                        if (member.TryGetComponent<DraggableStationery>(out var ds)) Destroy(ds);

                        member.transform.SetParent(groupRoot.transform, true);
                        processed.Add(member);
                    }

                    if (shouldBeKinematic)
                    {
                        rootRb.isKinematic = true;
                    }
                    else
                    {
                        rootRb.constraints = itemA.GetComponent<Rigidbody>().constraints;
                    }

                    rootRb.ResetCenterOfMass();
                }
                else
                {
                    if (itemA.TryGetComponent<DraggableStationery>(out var ds)) Destroy(ds);
                    if (itemA.TryGetComponent<Rigidbody>(out var rb))
                    {
                        rb.isKinematic = CheckIsTouchingFloor(itemA.gameObject);
                    }
                }
            }
        }

        private bool CheckIsTouchingFloor(GameObject obj)
        {
            Collider col = obj.GetComponent<Collider>();
            // 使用 OverlapCheck 檢查該 Collider 是否與地板層碰撞
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