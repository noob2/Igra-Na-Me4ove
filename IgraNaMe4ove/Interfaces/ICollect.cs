namespace IgraNaMe4ove.Interfaces
{
    using System.Collections.Generic;
    using GameObjects.Items;
    
    public interface ICollect
    {
        List<Item> Inventory { get; }

        void AddItemToInventory(Item item);
    }
}
