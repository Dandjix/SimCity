namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor.PackageManager.UI;
    using UnityEditor;
    using UnityEngine;

    [ExecuteAlways]
    [RequireComponent(typeof(Building))]
    public class Footprint : MonoBehaviour
    {
        //public FootprintData FootprintData = new FootprintData(new bool[0],0,Vector2Int.zero);
        public FootprintData FootprintData { 
            get => Building.BuildingSO.footprintData;
        }

        public Building Building {  get; private set; }

        private void Awake()
        {
            Building = GetComponent<Building>();
        }

        private void OnDrawGizmosSelected()
        {
            DrawFootprint();
        }

        private void DrawFootprint()
        {
            if (FootprintData == null)
            {
                Debug.Log("footprintData is null");
                return;
            }

            //Debug.Log("h w : "+FootprintData.Width+", "+FootprintData.Height);

            for (int i = 0; i < FootprintData.Width; i++)
            {
                for (int j = 0; j < FootprintData.Height; j++)
                {
                    bool isPartOfBuilding = FootprintData.footprint[i+j*FootprintData.Width];
                    if (isPartOfBuilding)
                    {
                        Gizmos.color = Color.red;

                        Vector3 position = new Vector3(i + FootprintData.offset.x + transform.position.x +0.5f, transform.position.y, j + FootprintData.offset.y + transform.position.z+0.5f);
                        Vector3 scale = new Vector3(1, 0.01f, 1);
                        Gizmos.DrawCube(position, scale);
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                        Vector3 blCorner = new Vector3(i + FootprintData.offset.x + transform.position.x , transform.position.y, j + FootprintData.offset.y + transform.position.z );
                        Vector3 tlCorner = new Vector3(i + FootprintData.offset.x + transform.position.x , transform.position.y, j + FootprintData.offset.y + transform.position.z + 1);
                        Vector3 brCorner = new Vector3(i + FootprintData.offset.x + transform.position.x + 1, transform.position.y, j + FootprintData.offset.y + transform.position.z );
                        Vector3 trCorner = new Vector3(i + FootprintData.offset.x + transform.position.x + 1, transform.position.y, j + FootprintData.offset.y + transform.position.z + 1);

                        Gizmos.DrawLine(blCorner, tlCorner);
                        Gizmos.DrawLine(tlCorner, trCorner);
                        Gizmos.DrawLine(trCorner, brCorner);
                        Gizmos.DrawLine(brCorner, blCorner);
                    }
                }
            }
        }
    

    
    }
}