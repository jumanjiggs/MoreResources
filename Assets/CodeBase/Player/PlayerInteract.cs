using CodeBase.Buildings;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.TryGetComponent(out Building building))
            // {
            //     if (building.typeResource == TypeResource.First || building.typeResource == TypeResource.Second)
            //         building.GiveAwayResources();
            //     if (building.typeResource == TypeResource.Second && !building.HaveResources()) 
            //         building.TakeResources();
            // }

            if (other.gameObject.TryGetComponent(out ReceivingResourcesWarehouse completedResources))
            {
                
            }
            if (other.gameObject.TryGetComponent(out ProducedResourcesWarehouse resources))
            {
                
            }
        }
    }
}