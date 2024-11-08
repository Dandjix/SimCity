namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class RoadConnection : MonoBehaviour
    {
        public Vector2Int GridPosition { get 
            {
                return new Vector2Int(Mathf.RoundToInt(transform.position.x - 0.5f), Mathf.RoundToInt(transform.position.z - 0.5f));    
            } 
        }
        public Vector3 WorldPosition
        {
            get
            {
                Vector3 pos = new Vector3(GridPosition.x+0.5f, transform.position.y, GridPosition.y+0.5f);
                return pos;
            }
        }

        private void OnDrawGizmosSelected()
        {

            Handles.color = Color.yellow;
            Handles.DrawSolidDisc(WorldPosition, Vector3.up, 0.5f);
        }
    }
}


