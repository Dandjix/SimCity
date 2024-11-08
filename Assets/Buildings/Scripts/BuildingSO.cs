namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    [CreateAssetMenu(fileName ="Building SO",menuName ="Buildings/Building SO")]
    public class BuildingSO : ScriptableObject {

        public static BuildingSO[] GetAll(BuildingCategory category)
        {
            BuildingSO[] cats = Resources.LoadAll<BuildingSO>("Resources/Buildings/SOs");

            if (category == null)
            {
                return cats;
            }

            var filteredCats = cats.Where(cat =>
            {
                return cat.buildingCategory == category;
            });

            return filteredCats.ToArray<BuildingSO>();
        }

        [SerializeField] public FootprintData footprintData = new FootprintData(5,5);

        public string buildingName;

        public BuildingCategory buildingCategory;

        public GameObject prefab;

        public Sprite menuIcon;

        public int base_buildTime_minutes;
        public int base_buildCost_cents;

    }
}
