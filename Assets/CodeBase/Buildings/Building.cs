using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Player;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    public enum TypeResource
    {
        First,
        Second,
        Third
    }

    public abstract class Building : MonoBehaviour
    {
        public TypeResource typeResource;
        public Resource prefabResource;
        public Transform positionResource;
        public Transform bagPosition;
        public ResourceHolder resourceHolder;
        public PlayerBag playerBag;
        public int currentResources;
        public int maxResources;
        public float generateTimeResource;
        public int availableFirstResource;

        protected Vector3 OffsetResource;
        
        protected readonly Stack<Resource> CurrentResource = new Stack<Resource>();

        private void Start()
        {
            GenerateResources();
        }

        protected abstract void GenerateResources();

        public abstract void TakeResources();

        protected void GiveAwayResources()
        {
            if (currentResources > 0)
                StartCoroutine(MoveResources());
        }

        protected abstract IEnumerator StartGenerateResources();

        private IEnumerator MoveResources()
        {
            StopCoroutine(StartGenerateResources());
            foreach (var resource in CurrentResource.ToList())
            {
                if (currentResources > 0 && playerBag.HaveFreeSpace())
                {
                    playerBag.IncreaseValue();
                    CurrentResource.Pop();
                    currentResources--;
                    resource.transform.parent = bagPosition;
                    resource.transform.DOLocalJump(Vector3.zero + playerBag.offsetBag, 5, 1, 1).OnComplete(() => {});
                    playerBag.IncreaseOffset();
                    DecreaseOffset();
                    resourceHolder.AddResourceType(resource.type);
                    resourceHolder.AddResource(resource);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            StartCoroutine(StartGenerateResources());
        }

        private void DecreaseOffset() => 
            OffsetResource.y -= 0.3f;

        protected void IncreaseOffset() => 
            OffsetResource.y += 0.3f;

        public abstract bool HaveResources();
    }
}