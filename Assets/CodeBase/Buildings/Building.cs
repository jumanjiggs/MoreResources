using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.ResourcesBuildings;
using CodeBase.UI;
using TMPro;
using UnityEngine;

namespace CodeBase.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        private const float OffsetResourceY = 0.3f;
        public Transform spawnPositionResource;
        public Resource resource;
        public TextMeshPro resourcesCountText;
        public UIFactory uiFactory;
        public float cooldownSpawnResource;
        public int maxCapacityResources;

        private Vector3 offsetResource;

        public readonly Stack<Resource> Resources = new Stack<Resource>();

        private IEnumerator SpawnResources()
        {
            while (CanGenerateResources())
            {
                CreateResources();
                yield return new WaitForSeconds(cooldownSpawnResource);
            }

            if (ReachedMaxCapacity()) 
                uiFactory.MaximumCapacityReachedUI(gameObject);
        }

        protected virtual void CreateResources()
        {
            var currentResource = Instantiate(resource, spawnPositionResource.position + offsetResource, Quaternion.identity);
            Resources.Push(currentResource);
            IncreaseOffset();
            UpdateResourceCountDisplay();
        }

        private void UpdateResourceCountDisplay() => 
            resourcesCountText.text = Resources.Count + "/" + maxCapacityResources;

        protected virtual bool CanGenerateResources() => 
            IsNotFullFactory();

        private bool IsNotFullFactory() => 
            Resources.Count < maxCapacityResources;
        
        private void IncreaseOffset() => 
            offsetResource.y += OffsetResourceY;

        public void DecreaseOffset() => 
            offsetResource.y -= OffsetResourceY;

        private bool ReachedMaxCapacity() => 
            Resources.Count >= maxCapacityResources;

        public void StartSpawn() => 
            StartCoroutine(SpawnResources());

        public void StopSpawn() =>
            StopAllCoroutines();
    }
}