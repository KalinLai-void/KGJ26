using System.Collections.Generic;
using Nori;
using UnityEngine;
using ZhengHua;

namespace KalinKonta.Stationery
{
    public class StationerySpawner : MonoBehaviour
    {
        public static StationerySpawner Instance;

        [Header("Spawn Settings")]
        [SerializeField] private LayerMask layer;
        [SerializeField] private List<GameObject> stationeryPrefabs;
        [SerializeField] private int spawnCount = 3;

        [Header("Draggable Settings")]
        [SerializeField] private GameObject pool;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private int totalValidCost = 10;
        private int leftCost;

        [Header("Audio")]
        [SerializeField] private AudioLibrary _audioLibrary;

        [Header("Hover Settings")]
        [SerializeField] private Outline.Mode OutlineMode = Outline.Mode.OutlineAll;
        [SerializeField] private Color OutlineColor = Color.yellow;
        [SerializeField] private float OutlineWidth = 1f;

        private BoxCollider spawnArea;
        private List<GameObject> spawnedItems = new List<GameObject>();


        private void OnDisable()
        {
            ClearOldObjs();
        }

        private void OnDestroy()
        {
            GameManager.OnStage2Finish?.RemoveAllListeners();
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            GameManager.OnStage1Start?.AddListener(GenerateStationery);
        }

        void Start()
        {
            Init();

            spawnArea = GetComponent<BoxCollider>();

            if (stationeryPrefabs != null)
            {
                foreach (var item in stationeryPrefabs)
                {
                    item.layer = Mathf.RoundToInt(Mathf.Log(layer.value, 2));
                }
            }
            GenerateStationery();
        }

        private void Init()
        {
            leftCost = totalValidCost;
            UIManager.Instance.UpdateCostText(leftCost);

            ClearOldObjs();
        }

        private void ClearOldObjs()
        {
            foreach (var item in spawnedItems)
            {
                if (item != null) Destroy(item);
            }
            spawnedItems.Clear();
        }

        public void GenerateStationery()
        {
            if (leftCost <= 0) return;

            ClearOldObjs();

            if (stationeryPrefabs == null || stationeryPrefabs.Count == 0) return;

            // Get boundary info of box collider
            Bounds bounds = spawnArea.bounds;
            float startX = bounds.min.x;
            float endX = bounds.max.x;
            float width = endX - startX;

            float step = width / (spawnCount + 1);

            List<GameObject> pool = new List<GameObject>(stationeryPrefabs);

            for (int i = 0; i < spawnCount; i++)
            {
                if (pool.Count == 0) break;

                float posX = startX + (step * (i + 1));
                Vector3 spawnPos = new Vector3(posX, bounds.center.y, bounds.center.z);

                int randomIndex = Random.Range(0, pool.Count);
                GameObject prefab = pool[randomIndex];

                GameObject go = Instantiate(prefab, spawnPos, prefab.transform.rotation);
                go.transform.SetParent(this.transform);

                if (!go.GetComponentInChildren<Outline>())
                {
                    var outline = go.AddComponent<Outline>();
                    outline.enabled = false;

                    StationeryHighlight highlighter = go.GetComponent<StationeryHighlight>();
                    if (highlighter == null) highlighter = go.AddComponent<StationeryHighlight>();

                    highlighter.OutlineMode = OutlineMode;
                    highlighter.OutlineColor = OutlineColor;
                    highlighter.OutlineWidth = OutlineWidth;
                }

                if (!go.GetComponent<DraggableStationery>())
                {
                    go.AddComponent<DraggableStationery>().rotationSpeed = rotationSpeed;
                }
                else
                {
                    go.GetComponent<DraggableStationery>().rotationSpeed = rotationSpeed;
                }

                spawnedItems.Add(go);
            }
        }

        public void SelectedObj(GameObject obj)
        {
            leftCost -= obj.GetComponent<DraggableStationery>().Cost;
            UIManager.Instance.UpdateCostText(leftCost);

            spawnedItems.Remove(obj);
            obj.transform.SetParent(pool.transform); // inpendent gameobject (remove from spawner)
            ClearOldObjs();
        }

        public void PlayPlaceSfx()
        {
            _audioLibrary?.PlaySfx(SfxId.Place);
        }

        public void PlayHoverSfx()
        {
            _audioLibrary?.PlaySfx(SfxId.StationeryHover);
        }

        public void PlayRotateTickSfx()
        {
            _audioLibrary?.PlaySfx(SfxId.StationeryRotate);
        }
    }
}