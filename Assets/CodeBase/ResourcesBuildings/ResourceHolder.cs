using System.Collections.Generic;
using System.Linq;
using CodeBase.Buildings;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.ResourcesBuildings
{
    public class ResourceHolder : MonoBehaviour
    {
        public Stack<Resource> Resources = new Stack<Resource>();
        public Transform bag;
        public Vector3 offsetBag;
        public int maxCapacity;


        public void AddResource(Resource resource)
        {
            resource.transform.parent = bag;
            resource.transform.DOLocalJump(Vector3.zero + offsetBag, 5, 1, 0.5f)
                .SetEase(Ease.Linear);
            IncreaseOffset();
            
            Resources.Push(resource);
        }

        public void ClearResourcesList(List<Resource> resourcesList)
        {
            List<Resource> tempList = Resources.ToList();
            
            foreach (var resource in resourcesList)
            {
                tempList.Remove(resource);
                DecreaseOffset();
            }
            
            tempList.Reverse();
            Resources = new Stack<Resource>(tempList);
        }

        public bool HaveFreeSpace() => 
            Resources.Count < maxCapacity;

        private void IncreaseOffset() => 
            offsetBag.y += 0.3f;

        private void DecreaseOffset() => 
            offsetBag.y -= 0.3f;

        public bool CheckStock() => 
            Resources.Count > 0;
    }
}