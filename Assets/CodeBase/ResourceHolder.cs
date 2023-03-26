using System.Collections.Generic;
using CodeBase.Buildings;
using UnityEngine;

namespace CodeBase
{
    public class ResourceHolder : MonoBehaviour
    {
        [HideInInspector] public List<TypeResource> resourcesType;
        public List<Resource> resources;

        public void AddResourceType(TypeResource resource) => 
            resourcesType.Add(resource);

        public void AddResource(Resource resource) => 
            resources.Add(resource);
    }
}