namespace Buildings
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Footprint))]
    public class FootprintEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            DrawOffsetHandle();
            DrawWidthHandle();
            DrawHeightHandle();
        }

        private void DrawOffsetHandle()
        {
            Footprint footprint = (Footprint)target;

            if (footprint.FootprintData == null)
            {
                Debug.Log("No FootprintData assigned.");
                return;
            }

            Vector3 handlePos = new Vector3(
                footprint.transform.position.x + footprint.FootprintData.offset.x +0.5f,
                footprint.transform.position.y, // Keep the original Y position
                footprint.transform.position.z + footprint.FootprintData.offset.y + 0.5f
            );

            Vector3 newPos = Handles.PositionHandle(handlePos, Quaternion.identity);


            Vector2Int newOffset = new Vector2Int(
                Mathf.RoundToInt(newPos.x - footprint.transform.position.x - 0.5f),
                Mathf.RoundToInt(newPos.z - footprint.transform.position.z - 0.5f) // Use z for the z-axis
            );


            if (newOffset != footprint.FootprintData.offset)
            {

                Undo.RecordObject(footprint.Building.BuildingSO, "Changed offset");
                footprint.FootprintData.offset = newOffset;

            }
        }
    
        private void DrawWidthHandle()
        {
            Footprint footprint = (Footprint)target;

            if (footprint.FootprintData == null)
            {
                Debug.Log("No FootprintData assigned.");
                return;
            }

            Vector3 handlePos = new Vector3(
                footprint.transform.position.x + footprint.FootprintData.Width + footprint.FootprintData.offset.x + 0.5f,
                footprint.transform.position.y, // Keep the original Y position
                footprint.transform.position.z + footprint.FootprintData.offset.y + 0.5f
            );

            Handles.color = Color.red;
            Vector3 newPos = Handles.FreeMoveHandle(handlePos, 1,Vector3.zero,Handles.CircleHandleCap);


            int newWidth =Mathf.RoundToInt( newPos.x - footprint.transform.position.x - footprint.FootprintData.offset.x - 0.5f);

            if(newWidth < 1)
                newWidth = 1;

            if (newWidth != footprint.FootprintData.Width)
            {

                Undo.RecordObject(footprint.Building.BuildingSO, "Changed width");
                footprint.FootprintData.Width = newWidth;

            }
        }

        private void DrawHeightHandle()
        {
            Footprint footprint = (Footprint)target;

            if (footprint.FootprintData == null)
            {
                Debug.Log("No FootprintData assigned.");
                return;
            }

            Vector3 handlePos = new Vector3(
                footprint.transform.position.x + footprint.FootprintData.offset.x + 0.5f,
                footprint.transform.position.y, // Keep the original Y position
                footprint.transform.position.z + footprint.FootprintData.Height + footprint.FootprintData.offset.y + 0.5f
            );

            Handles.color = Color.blue;
            Vector3 newPos = Handles.FreeMoveHandle(handlePos, 1, Vector3.zero, Handles.CircleHandleCap);


            int newHeight = Mathf.RoundToInt(newPos.z - footprint.transform.position.z - footprint.FootprintData.offset.y - 0.5f);

            if (newHeight < 1)
                newHeight = 1;

            if (newHeight != footprint.FootprintData.Height)
            {

                Undo.RecordObject(footprint.Building.BuildingSO, "Changed height");
                footprint.FootprintData.Height = newHeight;

            }
        }
    }
}
