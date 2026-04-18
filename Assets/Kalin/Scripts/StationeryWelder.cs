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

            HashSet<GameObject> processedObjects = new HashSet<GameObject>(); // Avoid repeats

            foreach (var itemA in allItems)
            {
                if (processedObjects.Contains(itemA.gameObject)) continue;

                List<GameObject> neighbors = FindTouchingItems(itemA.gameObject, allItems);

                if (neighbors.Count > 0)
                {
                    Rigidbody rootRb = itemA.GetComponent<Rigidbody>();
                    rootRb.isKinematic = false;
                    processedObjects.Add(itemA.gameObject);

                    foreach (var neighbor in neighbors)
                    {
                        if (processedObjects.Contains(neighbor)) continue;

                        Rigidbody neighborRb = neighbor.GetComponent<Rigidbody>();
                        if (neighborRb != null) Destroy(neighborRb);

                        var dragScript = neighbor.GetComponent<DraggableStationery>();
                        if (dragScript != null) dragScript.enabled = false;
                        neighbor.transform.SetParent(itemA.transform);

                        processedObjects.Add(neighbor);
                    }
                    rootRb.ResetCenterOfMass();
                }
                itemA.enabled = false;
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