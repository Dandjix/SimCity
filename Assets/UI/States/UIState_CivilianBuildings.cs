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

        [SerializeField] private BuildingConstructionSelector categorySelector;

        private Building toPlacePrefab = null;

        public override void Enter(UIStateInGame from)
        {
            canvas.gameObject.SetActive(true);
        }

        public override void Exit(UIStateInGame to)
        {
            canvas.gameObject.SetActive(false);
        }



        public void SelectCategory(BuildingCategory category)
        {
            toPlacePrefab = null;
            categorySelector.LoadCategory(category);
        }

        public void SelectBuilding(BuildingSO building)
        {

        }

        private Vector2Int? GetMouseTerrainPoint()
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Terrain");

            if (Physics.Raycast(ray, out hit, 10000, mask))
            {
                Vector3 hitPosition = hit.point;

                Vector2Int res = MapGrid.Instance.GetSquare(hitPosition);

                return res;
            }
            else
            {
                return null;
            }

        }

        private void Update()
        {
            Vector2Int? coordsToPlace = GetMouseTerrainPoint();
            if (coordsToPlace == null)
            {

            }
            else
            {

            }
        }
    }

}