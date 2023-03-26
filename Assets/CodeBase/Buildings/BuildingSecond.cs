using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingSecond : Building
    {
        [SerializeField] private Transform receivedPosition;

        protected override void GenerateResources()
        {
            StartCoroutine(StartGenerateResources());
        }

        public override void TakeResources()
        {
            StartCoroutine(TakeResourcesFromPlayer());
        }

        protected override IEnumerator StartGenerateResources()
        {
            while (currentResources < maxResources && HaveResources())
            {
                currentResources++;
                availableFirstResource--;
                Resource resource =
                    Instantiate(prefabResource, positionResource.position + OffsetResource, Quaternion.identity);
                CurrentResource.Push(resource);
                OffsetResource.y += 0.3f;
                yield return new WaitForSeconds(generateTimeResource);
            }
        }

        private IEnumerator TakeResourcesFromPlayer()
        {
            if (DontHaveFirstResource())
            {
                for (int i = 0; i < resourceHolder.resources.Count; i++)
                {
                    if (resourceHolder.resourcesType[i] == TypeResource.First)
                    {
                        resourceHolder.resources[i].transform.parent = receivedPosition;
                        resourceHolder.resources[i].transform.DOLocalJump(Vector3.zero, 2, 1, 1f).SetEase(Ease.Linear);
                        Destroy(resourceHolder.resources[i], 0.15f);
                        availableFirstResource++;
                        playerBag.DecreaseCapacity();
                        playerBag.DecreaseOffset();
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                
                resourceHolder.resourcesType.Clear();
                resourceHolder.resources.Clear();
                yield return new WaitForSeconds(1f);
                
                GenerateResources();
            }
            else
            {
               GiveAwayResources();
            }
            
        }

        private bool DontHaveFirstResource() => 
            availableFirstResource == 0 && currentResources == 0;

        public override bool HaveResources() =>
            availableFirstResource > 0;
    }
}