namespace InventoryAPI
{
    /// <summary>
    /// Type of Notification Being generated..
    /// </summary>
    public enum NotificationType
    {
        ItemRemoved,
        ItemExpired
    };

    /// <summary>
    /// Type of Inventory stored in the system...
    /// </summary>
    public enum InventoryType
    {
        PersonalComputer,
        Printer,
        AppServer,
        DbServer,
        LapTop
    };
}
