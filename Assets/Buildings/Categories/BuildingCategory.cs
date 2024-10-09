namespace Buildings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    [CreateAssetMenu(fileName ="BuildingCategory", menuName ="Buildings/Building category")]
    public class BuildingCategory : ScriptableObject, IComparable<BuildingCategory>
    {
        //public new string name;
        public string description;
        public Sprite icon;
        public Color color = Color.white;
        /// <summary>
        /// lowest to the left
        /// </summary>
        public byte order = 0;

        public static BuildingCategory[] getAll()
        {
            var cats = Resources.LoadAll<BuildingCategory>("Buildings/Categories");
            Array.Sort(cats);
            return cats;
        }

        public int CompareTo(BuildingCategory other)
        {
            return order.CompareTo(other.order);
        }
    }
}