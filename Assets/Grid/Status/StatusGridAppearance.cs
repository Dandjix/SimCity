namespace StatusGrid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Timeline;

    public class StatusGridAppearance : MonoBehaviour
    {
        public static StatusGridAppearance Instance { get; private set; }

        [SerializeField] private HeightType heightType;

        [SerializeField] private float heightOffset = 75;

        [SerializeField] private Material baseMaterial;
        public Material BaseMaterial { get => baseMaterial; }

        public readonly Dictionary<StatusType, Color> ColorsForStatusTypes = new Dictionary<StatusType, Color>();

        private Dictionary<Vector2Int, StatusGridMarker> markers = new Dictionary<Vector2Int,StatusGridMarker>();



        [SerializeField] private GameObject baseMarker;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("more than one StatusGridAppearance Instance.");
            }
            Instance = this;

            Color[] colors = new Color[8];
            for (int i = 0; i < 8; i++)
            {
                float hue = i / 8.0f;
                colors[i] = Color.HSVToRGB(hue, 1f, 1f);
                //Debug.Log($"Color {i}: {colors[i]}");
            }

            for (int i = 0; i < 8; i++)
            {
                ColorsForStatusTypes.Add((StatusType)i, colors[i]);
            }
        }

        private void OnEnable()
        {
            StatusGrid.Instance.StatusChanged += StatusChanged;
            GenerateStatuses();
        }

        private void OnDisable()
        {
            StatusGrid.Instance.StatusChanged -= StatusChanged;
            DeleteStatuses();
        }

        private void GenerateStatuses()
        {
            foreach(var square in MapGrid.Instance.GetAllSquares())
            {
                StatusChanged(square, new Status(), StatusGrid.Instance.GetStatus(square));
            }
        }

        private void DeleteStatuses()
        {
            foreach(var pair in markers)
            {
                Destroy(markers[pair.Key].gameObject);
            }
            markers.Clear();
        }

        private void StatusChanged(Vector2Int position, Status oldStatus, Status newStatus)
        {
            //Debug.Log("status changed ! ");
            if(newStatus.statusByte == 0)
            {
                TryDeleteMarker(position);
            }
            else if (!markers.ContainsKey(position))
            {
                CreateMarker(position, newStatus);
            }
            else
            {
                ModifyMarker(position, newStatus);
            }
        }

        private void TryDeleteMarker(Vector2Int position)
        {
            StatusGridMarker marker;
            if(markers.TryGetValue(position,out marker))
            {
                Destroy(marker.gameObject);
                markers.Remove(position);
            }
        }

        private void CreateMarker(Vector2Int position, Status status)
        {
            GameObject marker = Instantiate(baseMarker,transform);
            StatusGridMarker statusGridMarker = marker.GetComponent<StatusGridMarker>();

            Vector3 center;

            if ((heightType == HeightType.World))
            {
                Vector2 centerNoHeight = MapGrid.Instance.GetCenterNoHeight(position);
                center = new Vector3(centerNoHeight.x, heightOffset, centerNoHeight.y);
            }
            else // if Terrain
            {
                center = MapGrid.Instance.GetCenter(position);
                center = new Vector3(center.x, center.y+ heightOffset, center.z);
            }

            marker.transform.position = center;
            statusGridMarker.Status = status;

            markers.Add(position, statusGridMarker);
        }

        private void ModifyMarker(Vector2Int position, Status status) 
        {
            StatusGridMarker marker;
            if (markers.TryGetValue(position, out marker))
            {
                marker.Status = status;
            }
        }
    }

    public enum HeightType
    {
        Terrain,
        World
    }
}