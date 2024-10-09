namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor.PackageManager.UI;
    using UnityEditor;
    using UnityEngine;
    using System;

    [ExecuteAlways]
    [RequireComponent(typeof(Building))]
    public class Footprint : MonoBehaviour
    {
        //public FootprintData FootprintData = new FootprintData(new bool[0],0,Vector2Int.zero);
        public FootprintData FootprintData {
            get 
            { 
                if(Building.BuildingSO == null)
                    return null;

                return Building.BuildingSO.footprintData; 
            }
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

            foreach(FootprintTile tile in EnumerateAllTiles())
            {
                bool isPartOfBuilding = tile.isPartOfBuilding;
                if (isPartOfBuilding)
                {
                    Handles.color = Color.red;

                    Vector3 position = new Vector3(tile.x+0.5f, transform.position.y, tile.y+0.5f);

                    Handles.DrawSolidDisc(position, Vector3.up, 0.5f);
                }

            }
        }
    
        public IEnumerable<FootprintTile> EnumerateAllTiles()
        {
            if(FootprintData == null)
            {
                yield break;
            }

            //float yAngle = transform.rotation.eulerAngles.y;



            //for (int i = 0; i < FootprintData.Width; i++)
            //{
            //    for (int j = 0; j < FootprintData.Height; j++)
            //    {
            //        int x, y;

            //        switch (yAngle)
            //        {
            //            case (0):
            //                x = i + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
            //                y = j + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
            //                break;
            //            case 90:
            //                // 90-degree rotation: swap i and j, reverse i
            //                x = j + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
            //                y = -i + 1 + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
            //                break;
            //            case 180:
            //                // 180-degree rotation: reverse both i and j
            //                x = -i + 1 + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
            //                y = -j + 1 + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
            //                break;
            //            case 270:
            //                // 270-degree rotation: swap i and j, reverse j
            //                x = -j + 1 + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
            //                y = i + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
            //                break;
            //            default:
            //                yield break;
            //        }

            // free optimisation (probably insignificant)

            float yAngle = transform.rotation.eulerAngles.y;

            Func<int, int, int> calculateX, calculateY;

            switch (yAngle)
            {
                case 0:
                    calculateX = (i, j) => i + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
                    calculateY = (i, j) => j + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
                    break;
                case 90:
                    calculateX = (i, j) => j + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
                    calculateY = (i, j) => -i + 1 + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
                    break;
                case 180:
                    calculateX = (i, j) => -i + 1 + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
                    calculateY = (i, j) => -j + 1 + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
                    break;
                case 270:
                    calculateX = (i, j) => -j + 1 + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x);
                    calculateY = (i, j) => i + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z);
                    break;
                default:
                    yield break;
            }


            for (int i = 0; i < FootprintData.Width; i++)
            {
                for (int j = 0; j < FootprintData.Height; j++)
                {
                    int x = calculateX(i, j);
                    int y = calculateY(i, j);

                    var tile =
                            new FootprintTile
                            (
                                //i + FootprintData.offset.x + Mathf.RoundToInt(transform.position.x),
                                //j + FootprintData.offset.y + Mathf.RoundToInt(transform.position.z),
                                x,
                                y,
                                i + j * FootprintData.Width,
                                FootprintData.footprint[i + j * FootprintData.Width]
                            );
                        yield return tile;
                }
            }
        }
    }

    public struct FootprintTile
    {
        public int footprintIndex;
        public int x, y;
        public bool isPartOfBuilding;
        public FootprintTile(int x, int y,int footprintIndex, bool isPartOfBuilding)
        {
            this.x = x; 
            this.y = y;
            this.footprintIndex = footprintIndex;
            this.isPartOfBuilding = isPartOfBuilding;
        }
    }
}