namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName ="Building SO",menuName ="Buildings/Building SO")]
    public class BuildingSO : ScriptableObject {

        [SerializeField] public FootprintData footprintData = new FootprintData(5,5);

        public string buildingName;

        public BuildingCategory buildingCategory;

        public GameObject prefab;

        public Sprite menuIcon;

        public int base_buildTime_minutes;
        public int base_buildCost_cents;

    }
}
