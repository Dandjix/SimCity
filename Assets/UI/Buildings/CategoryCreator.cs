namespace IngameUI
{

    using Buildings;
    using IngameUI;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CategoryCreator : MonoBehaviour
    {

        [SerializeField] private GameObject categoryButton;

        void Awake()
        {
            BuildingCategory[] categories = BuildingCategory.GetAll();
            DestroyChildren();

            foreach (BuildingCategory category in categories)
            {
                GameObject newButton = Instantiate(categoryButton);
                newButton.transform.SetParent(this.transform, false);
                newButton.GetComponent<BuildingCategoryOption>().BuildingCategory = category;
            }
        }

        private void DestroyChildren()
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            foreach (GameObject child in children)
            {
                Destroy(child);
            }
        }
    }

}
