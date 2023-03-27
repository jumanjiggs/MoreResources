using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.ResourcesBuildings;
using CodeBase.UI;
using DG.Tweening;
using UnityEngine;
using Resource = CodeBase.ResourcesBuildings.Resource;

namespace CodeBase.Warehouse
{
    public class ReceivingResourcesWarehouse : Warehouse
    {
        public List<TypeResource> necessaryResources;
        public Transform receivedPosition;
        public UIFactory uIFactory;
        public int maxCapacity;

        private Stack<Resource> collectedResources = new Stack<Resource>();
        private Vector3 placeOffset;
        
        public bool HasAvailableResources()
        {
            bool hasAllTypes = necessaryResources.All(x => collectedResources.ToList().Find(y => y.type == x));
            return collectedResources.Count > 0 && hasAllTypes;
        }

        public virtual void GiveResourceToProduce()
        {
            List<Resource> tempList = collectedResources.ToList();
            
            foreach (var typeResource in necessaryResources)
            {
                Resource resource = tempList.Find(x => x.type == typeResource);
                tempList.Remove(resource);
                Destroy(resource.gameObject);
                UpdateResourceCount(tempList, maxCapacity);
            }
            
            tempList.Reverse();
            collectedResources = new Stack<Resource>(tempList);
            placeOffset.y -= 0.06f;
            ReplaceResources();
            
            if (IsEmptyResources()) 
                uIFactory.ShowOutNecessaryResources();
        }

        private void ReplaceResources()
        {
            placeOffset = Vector3.zero;
            foreach (var resource in collectedResources.ToList())
            {
                resource.transform.localPosition = Vector3.zero + placeOffset;
                placeOffset.y += 0.06f;
            }
        }

        public List<Resource> TakeResourcesFromPlayer(List<Resource> resources)
        {
            var tempList = new List<Resource>();
            for (int i = 0; i < resources.Count; i++)
            {
                var resource = resources[i];
                if (CheckCapacity(collectedResources.Count + tempList.Count) &&
                    necessaryResources.Contains(resource.type) && !IfTypeIsMax(resource.type, tempList))
                {
                    tempList.Add(resource);
                    UpdateResourceCount(tempList, maxCapacity);
                }
            }

            if (tempList.Count > 0)
            {
                foreach (var resource in tempList)
                    collectedResources.Push(resource);
                building.StopSpawn();
                StartCoroutine(TakeResources(tempList));
            }
            return tempList;
        }
        
        
        private bool IfTypeIsMax(TypeResource resourceType, List<Resource> tempList)
        {
            var total = new List<Resource>();
            total.AddRange(tempList);
            total.AddRange(collectedResources);
            var max = maxCapacity / necessaryResources.Count;
            var totalOfType = total.Count(resource => resource.type == resourceType);
          
            return totalOfType >= max;
        }

        private IEnumerator TakeResources(List<Resource> resources)
        {
            foreach (var res in resources)
            {
                res.transform.parent = receivedPosition;
                res.transform.DOLocalJump(Vector3.zero + placeOffset, 2, 1, 0.5f).SetEase(Ease.Linear);
                placeOffset.y += 0.06f;
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(1f);
            building.StartSpawn();
        }

        private bool CheckCapacity(int newCapacity) =>
            newCapacity <= maxCapacity;

        private bool IsEmptyResources() => 
            collectedResources.Count <= 0;
    }
}