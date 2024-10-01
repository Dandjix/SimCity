namespace StatusGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class StatusGridAppearance : MonoBehaviour
    {
        private bool startHasBeenCalled = false;

        public static StatusGridAppearance Instance { get; private set; }

        [SerializeField] private HeightType heightType;

        [SerializeField] private float heightOffset = 75;

        [SerializeField] private Texture2D texture;

        public readonly Dictionary<StatusType, Color> ColorsForStatusTypes = new Dictionary<StatusType, Color>();

        [SerializeField] private UnityEngine.Rendering.Universal.DecalProjector projector;


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

        private void Start()
        {
            startHasBeenCalled = true;

            SetupProjector();
            GenerateTexture();
        }

        private void OnEnable()
        {
            StatusGrid.Instance.StatusChanged += StatusChanged;
            StatusGrid.Instance.StatusCommited += CommitTexture;

            projector.gameObject.SetActive(true);

            if(startHasBeenCalled)
            {
                GenerateTexture();
            }
            //GenerateTexture();
        }

        private void OnDisable()
        {
            StatusGrid.Instance.StatusChanged -= StatusChanged;
            StatusGrid.Instance.StatusCommited -= CommitTexture;

            if(projector != null && projector.isActiveAndEnabled)
            {
                projector.gameObject?.SetActive(false);
            }


            //DeleteTexture();
        }

        private void SetupProjector()
        {
            Vector2 center = MapGrid.Instance.Center;
            Vector3 projectorPosition;
            switch (heightType)
            {
                case HeightType.Terrain:
                    //projectorPosition = new Vector3(center.x, heightOffset, center.y);
                    throw new NotImplementedException("need to write code to get height of any position bozo");
                    //break;
                //case HeightType.World:
                default:
                    projectorPosition = new Vector3(center.x, heightOffset, center.y);
                    break;
            }

            projector.transform.position = projectorPosition;
            projector.size = new Vector3(MapGrid.Instance.DimensionX,MapGrid.Instance.DimensionY,projector.size.z);
        }

        private void GenerateTexture()
        {
            //Debug.Log("generating texture... magGrid dims : "+ MapGrid.Instance.DimensionX+", "+MapGrid.Instance.DimensionY);
            texture = new Texture2D(MapGrid.Instance.DimensionX, MapGrid.Instance.DimensionY);
            texture.filterMode = FilterMode.Point;
            
            foreach (var square in MapGrid.Instance.GetAllSquares())
            {
                var statusOfSquare = StatusGrid.Instance.GetStatus(square);

                Color color = colorFor(statusOfSquare);

                texture.SetPixel(square.x, square.y, color);
            }
            texture.Apply();
            projector.material.SetTexture("Base_Map",texture);
            //Debug.Log("texture applied");
        }

        private Color colorFor(Status statusOfSquare)
        {
            List<Color> colors = new List<Color>();
            foreach (StatusType status in Enum.GetValues(typeof(StatusType)))
            {
                if (statusOfSquare.IsStatus(status))
                    colors.Add(ColorsForStatusTypes[status]);
            }
            if(colors.Count <= 0)
            {
                return Color.clear;
            }
            Color color = blend(colors);
            return color;
        }

        private static Color blend(List<Color> colors)
        {
            Color finalColor = Color.black;
            int colorCount = colors.Count;

            if (colorCount > 0)
            {
                float r = 0f;
                float g = 0f;
                float b = 0f;

                // Sum up the individual RGBA components
                foreach (Color color in colors)
                {
                    r += color.r;
                    g += color.g;
                    b += color.b;
                }

                // Calculate the average for each component
                r /= colorCount;
                g /= colorCount;
                b /= colorCount;


                // Create the final color with averaged components
                return new Color(r, g, b, 1); // Using the RGBA components
            }
            return Color.clear;
        }

        private void StatusChanged(Vector2Int position, Status oldStatus, Status newStatus)
        {
            //Debug.Log("detected change !");

            Color color = colorFor(newStatus);

            texture.SetPixel(position.x,position.y, color);

        }

        private void CommitTexture()
        {
            texture.Apply();

            projector.material.SetTexture("Base_Map", texture);
        }
    }

    public enum HeightType
    {
        Terrain,
        World
    }
}