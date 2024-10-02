namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class FootprintData
    {
        // Use a list of lists to make the footprint editable in the Unity editor.

        [HideInInspector]
        [SerializeField]
        private int width, height;

        [HideInInspector]
        public bool[] footprint;

        [HideInInspector]
        [SerializeField]
        public Vector2Int offset;

        public int Width {  
            get { 
                return width; 
            } 
            set {
                footprint = Fit(footprint, width, height, value, height);
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                footprint = Fit(footprint, width, height, width, value);
                height = value;
            }
        }

        private bool[] Fit(bool[] oldFootprint,int oldWidth, int oldHeight, int newWidth, int newHeight)
        {
            bool[,] old2D = new bool[oldWidth, oldHeight];
            for (int i = 0; i < oldWidth; i++)
            {
                for (int j = 0; j < oldHeight; j++)
                {
                    old2D[i,j] = oldFootprint[i + j*oldWidth];
                }
            }

            bool[,] new2D = new bool[newWidth, newHeight];
            for (int i = 0; i < old2D.GetLength(0) && i < new2D.GetLength(0); i++)
            {
                for (int j = 0; j < old2D.GetLength(1) && j < new2D.GetLength(1); j++)
                {
                    new2D[i,j] = old2D[i,j];
                }
            }

            bool[] newFootprint = new bool[newWidth*newHeight];

            for(int i = 0;i < new2D.GetLength(0);i++)
            {
                for(int j = 0;j < new2D.GetLength(1);j++)
                {
                    newFootprint[i+j*newWidth] = new2D[i,j];
                }
            }

            return newFootprint;
        }


        public FootprintData(int width,int height)
        {
            this.width = width;
            this.height = height;
            footprint = new bool[width * height];
            offset = new Vector2Int(0,0);
        }

    }
}
