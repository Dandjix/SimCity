namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingPlacer : MonoBehaviour
    {
        private Transform buildingSilhouette;

        private BuildingOption buildingOption 
        {
            get => BuildingSelector.Instance.BuildingOption;
        }

        private void OnEnable()
        {
            BuildingSelector.Instance.OnBuildingOptionChanged += BuildingOnptionChanged;

            BuildingOnptionChanged(BuildingSelector.Instance.BuildingOption);

            //BuildingSilhouette.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            BuildingSelector.Instance.OnBuildingOptionChanged -= BuildingOnptionChanged;

            buildingSilhouette.gameObject.SetActive(false);
        }

        private void BuildingOnptionChanged(BuildingOption option)
        {
            if (buildingSilhouette != null)
            {
                Destroy(buildingSilhouette.gameObject);
            }
            if (option == null)
            {
                return;
            }

            GameObject silhouetteGO = Instantiate(option.BuildingSO.prefab);
            silhouetteGO.name = "building placement silhouette";

            buildingSilhouette = silhouetteGO.transform;
        }

        private void Update()
        {
            if (buildingOption == null)
            {
                return;
            }


            Vector2Int? coordsToPlace = GetMouseTerrainPoint();
            if (coordsToPlace == null)
            {
                buildingSilhouette.gameObject.SetActive(false);
            }
            else
            {
                Vector2 anchor = buildingOption.BuildingSO.prefab.GetComponent<Buildings.Building>().Anchor;

                Vector2 position = coordsToPlace.Value;// + anchor;

                Vector2Int intPosition = new Vector2Int(Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y));

                buildingSilhouette.transform.position = MapGrid.Instance.GetCenter(intPosition);

                buildingSilhouette.gameObject.SetActive(true);
            }
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
    }
}


