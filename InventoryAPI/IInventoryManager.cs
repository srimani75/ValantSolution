namespace InventoryAPI
{
    /// <summary>
    /// Interface for Inventory manager .....
    /// </summary>
    public interface IInventoryManager
    {
        bool AddItem(InventoryItem prmItem);
        bool RemoveItem(string prmLabel);
        void Notify(NotificationType prmNotificationType, string prmMessage);
        InventoryItem[] GetItems();
        InventoryItem GetItem(string prmId);
    }
}
