using System.Collections;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingFirst : Building
    {
        protected override void GenerateResources()
        {
            StartCoroutine(StartGenerateResources());
        }

        public override void TakeResources()
        {
            //
        }

        protected override IEnumerator StartGenerateResources()
        {
            while (currentResources < maxResources)
            {
                currentResources++;
                Resource resource = Instantiate(prefabResource, positionResource.position + OffsetResource, Quaternion.identity);
                IncreaseOffset();
                CurrentResource.Push(resource);
                yield return new WaitForSeconds(generateTimeResource);
            }
        }

        public override bool HaveResources()
        {
            throw new System.NotImplementedException();
        }
    }
}