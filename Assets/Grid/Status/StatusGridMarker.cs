namespace StatusGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    public class StatusGridMarker : MonoBehaviour
    {
        private static Dictionary<Status, Material> materials = new Dictionary<Status, Material>();

        private Status status;
        public Status Status 
        {  
            get { return status; } 
            set { 
                status = value;
                UpdateAppearance();
            } 
        }

        private void UpdateAppearance()
        {


            var decalProjector = GetComponent<DecalProjector>();

            Material material = GetmaterialForStatus(status);

            decalProjector.material = material;

        }

        private Material GetmaterialForStatus(Status status)
        {
            if(materials.ContainsKey(status))
            {
                return materials[status];
            }

            List<Color> colors = new List<Color>();
            foreach (StatusType statusType in Enum.GetValues(typeof(StatusType)))
            {
                if(status.IsStatus(statusType))
                    colors.Add(StatusGridAppearance.Instance.ColorsForStatusTypes[statusType]);
            }



            Color finalColor = blend(colors);

            //Debug.Log("blended " + colors.Count+" colors, got "+finalColor);

            Material material = Instantiate(StatusGridAppearance.Instance.BaseMaterial);
            material.SetColor("_Color",finalColor);

            materials.Add(status, material);

            return material;
        }

        private Color blend(List<Color> colors)
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
    }
}