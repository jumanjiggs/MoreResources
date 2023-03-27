using System.Collections;
using System.Linq;
using CodeBase.ResourcesBuildings;
using UnityEngine;

namespace CodeBase.Warehouse
{
    public class ProducedResourcesWarehouse : Warehouse
    {
        public void GiveResourceToPlayer(ResourceHolder resourceHolder) => 
            StartCoroutine(GiveResources(resourceHolder));

        private IEnumerator GiveResources(ResourceHolder resourceHolder)
        {
            building.StopSpawn();
            foreach (var currentResource in building.Resources.ToList()
                         .Where(currentResource => HaveResources() && resourceHolder.HaveFreeSpace()))
            {
                resourceHolder.AddResource(currentResource);
                building.DecreaseOffset();
                building.Resources.Pop();
                UpdateResourceCount(building.Resources.ToList(), building.maxCapacityResources);
                yield return new WaitForSeconds(0.2f);
                
            }

            yield return new WaitForSeconds(1f);
            building.StartSpawn();
        }

        public bool HaveResources() => 
            building.Resources.Count > 0;
    }
}