using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAPI
{
    public class InventoryManager : IInventoryManager
    {
        readonly ConcurrentDictionary<string,InventoryItem>  _dctInventory = new ConcurrentDictionary<string, InventoryItem>();
        /// <summary>
        /// Adds a new tiem to the inventory collection....
        /// </summary>
        /// <param name="prmItem"></param>
        /// <returns></returns>
        public bool AddItem(InventoryItem prmItem)
        {
            return prmItem != null && _dctInventory.TryAdd(prmItem.InventoryLabel, prmItem);
        }

        /// <summary>
        /// Roves any given item form the Inventory system
        /// </summary>
        /// <param name="prmLabel"></param>
        /// <returns></returns>
        public bool RemoveItem(string prmLabel)
        {
            if (prmLabel == null) return false;//No notification if the input is null..
            InventoryItem prmItem;
            var retVal = _dctInventory.TryRemove(prmLabel ,out prmItem);
            var val = retVal ? "" : "not";
            string prmMessage = $"The item with id: {prmLabel} was {val}  removed from the inventory system";
            Notify(NotificationType.ItemRemoved, prmMessage);
            return retVal;
        }

        /// <summary>
        /// Notifies the User(s) of a given event (removal or expiry)
        /// </summary>
        /// <param name="prmNotificationType"></param>
        /// <param name="prmMessage"></param>
        public void Notify(NotificationType prmNotificationType, string prmMessage)
        {
            //TODO: Add various kinds of notification like email, txt etc..
            Console.WriteLine($"Improtant Message: {prmNotificationType} : {prmMessage} .");
        }

        /// <summary>
        /// Gets allt he Items from the Inventory system..
        /// </summary>
        /// <returns></returns>
        public InventoryItem[] GetItems()
        {
            return _dctInventory.Values.ToArray();
        }

        /// <summary>
        /// Gets any given item fromt he system..
        /// </summary>
        /// <param name="prmId"></param>
        /// <returns></returns>
        public InventoryItem GetItem(string prmId)
        {
            InventoryItem itm;
            _dctInventory.TryGetValue(prmId, out itm);
            return itm;
        }

        /// <summary>
        /// Checks the system for expired items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InventoryItem> CheckItems()
        {
            int val = _dctInventory.Values.ElementAt(0).ExperiationDate.CompareTo(DateTime.Now);

            return _dctInventory.Values.Select(itm => new {itm, val = itm.ExperiationDate.CompareTo(DateTime.Now)})
                .Where(@t => @t.val < 0)
                .Select(@t => @t.itm).ToList();
        }

        /// <summary>
        /// Sends notification for the expired items...
        /// </summary>
        public void NotifyExpiredItems()
        {
            IEnumerable<InventoryItem> prmItems = CheckItems();
            foreach (var prmMessage in prmItems.Select(itm => $"The item with id: {itm.InventoryLabel} with Expiration Date {itm.ExperiationDate} has been expired.Please do the needful asap"))
            {
                Notify(NotificationType.ItemExpired, prmMessage);
            }
        }
    }
}
