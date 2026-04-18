using KalinKonta.Stationery;
using System.Collections.Generic;
using UnityEngine;

namespace KalinKonta.Stationery
{
    public class StationeryWelder : MonoBehaviour
    {
        private void OnEnable()
        {
            StationerySpawner.Instance.enabled = false;
            ExecuteWeld();
        }

        public void ExecuteWeld()
        {
            DraggableStationery[] allItems = FindObjectsByType<DraggableStationery>(FindObjectsSortMode.None);

            HashSet<GameObject> processed = new HashSet<GameObject>(); // Avoid repeats

            foreach (var itemA in allItems)
            {
                if (processed.Contains(itemA.gameObject)) continue;

                List<GameObject> neighbors = FindTouchingItems(itemA.gameObject, allItems);

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

                    neighbors.Add(itemA.gameObject);

                    foreach (var member in neighbors)
                    {
                        if (processed.Contains(member)) continue;

                        if (member.TryGetComponent(out Rigidbody mRb)) Destroy(mRb);
                        if (member.TryGetComponent<DraggableStationery>(out var ds)) Destroy(ds);

                        member.transform.SetParent(groupRoot.transform, true);
                        processed.Add(member);
                    }

                    rootRb.ResetCenterOfMass();
                }
                else
                {
                    if (itemA.TryGetComponent<DraggableStationery>(out var ds)) Destroy(ds);
                    if (itemA.TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = false;
                }
            }
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