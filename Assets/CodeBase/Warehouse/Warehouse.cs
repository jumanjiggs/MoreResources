using System.Collections.Generic;
using CodeBase.Buildings;
using CodeBase.ResourcesBuildings;
using TMPro;
using UnityEngine;

namespace CodeBase.Warehouse
{
    public abstract class Warehouse : MonoBehaviour
    {
        public Building building;
        public TextMeshPro resourcesCountText;
        

        protected void UpdateResourceCount(List<Resource> resources, int maxCapacity) => 
            resourcesCountText.text = resources.Count + "/" + maxCapacity;
    }
}