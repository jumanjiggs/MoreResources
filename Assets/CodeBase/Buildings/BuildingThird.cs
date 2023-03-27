using CodeBase.Warehouse;

namespace CodeBase.Buildings
{
    public class BuildingThird : Building
    {
        public ReceivingResourcesWarehouse receivingWarehouse;

        protected override bool CanGenerateResources() => 
            base.CanGenerateResources() && receivingWarehouse.HasAvailableResources();

        protected override void CreateResources()
        {
            base.CreateResources();
            receivingWarehouse.GiveResourceToProduce();
        }
    }
}