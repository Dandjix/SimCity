namespace UIInGameStateMachine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;
    using IngameUI;

    public class UIState_CivilianBuildings : UIStateInGame
    {
        [SerializeField] private Canvas canvas;

        //[SerializeField] private BuildingCategorySelector categorySelector;

        // public static UIState_CivilianBuildings Instance { get; private set; }

        //private void Start()
        //{
        //    Instance = this;
        //}

        public override void Enter(UIStateInGame from)
        {
            canvas.gameObject.SetActive(true);
        }

        public override void Exit(UIStateInGame to)
        {
            canvas.gameObject.SetActive(false);
        }

        private CategoryOption categoryButton;

        //private Vector2Int? GetMouseTerrainPoint()
        //{

        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    LayerMask mask = LayerMask.GetMask("Terrain");

        //    if (Physics.Raycast(ray, out hit, 10000, mask))
        //    {
        //        Vector3 hitPosition = hit.point;

        //        Vector2Int res = MapGrid.Instance.GetSquare(hitPosition);

        //        return res;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIInGameStateMachine.Set(UIInGameStateMachine.UIState_Civilian);
            }

            //if (buildingSOToPlace ==  null)
            //{
            //    return;
            //}


            //Vector2Int? coordsToPlace = GetMouseTerrainPoint();
            //if (coordsToPlace == null)
            //{
            //    buildingToPlaceSilhouette.gameObject.SetActive(false);
            //}
            //else
            //{
            //    buildingToPlaceSilhouette.gameObject.SetActive(true);
            //    buildingToPlaceSilhouette.transform.position = MapGrid.Instance.GetCenter(coordsToPlace.Value);
            //}
        }
    }

}