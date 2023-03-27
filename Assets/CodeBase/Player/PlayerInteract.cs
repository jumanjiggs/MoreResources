using System;
using System.Linq;
using CodeBase.Buildings;
using CodeBase.ResourcesBuildings;
using CodeBase.Warehouse;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public ResourceHolder resourcesHolder;

        private bool isEnteredProduced;
        private bool isEnteredReceiving;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ReceivingResourcesWarehouse warehouseReceiver) &&
                !isEnteredReceiving)
            {
                isEnteredReceiving = true;
                if (!resourcesHolder.CheckStock())
                    return;
                var takenResources = warehouseReceiver.TakeResourcesFromPlayer(resourcesHolder.Resources.ToList());
                if (takenResources.Count > 0)
                    resourcesHolder.ClearResourcesList(takenResources);
            }

            if (other.gameObject.TryGetComponent(out ProducedResourcesWarehouse warehouse) && !isEnteredProduced)
            {
                isEnteredProduced = true;
                if (warehouse.HaveResources())
                    warehouse.GiveResourceToPlayer(resourcesHolder);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ReceivingResourcesWarehouse warehouseReceiver))
                isEnteredReceiving = false;

            if (other.gameObject.TryGetComponent(out ProducedResourcesWarehouse warehouse))
                isEnteredProduced = false;
        }
    }
}