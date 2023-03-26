using System.Collections;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingThird : Building
    {
        protected override void GenerateResources()
        {
            
        }
        
        public override void TakeResources()
        {
            
        }

        protected override IEnumerator StartGenerateResources()
        {
            throw new System.NotImplementedException();
        }

        public override bool HaveResources()
        {
            throw new System.NotImplementedException();
        }
    }
}