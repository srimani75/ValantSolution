using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InventoryAPITests
{
    [TestClass]
    public class ApiTests
    {
        private InventoryManager _mgr = null;
        private InventoryItem _itm = null;
        

        [TestInitialize]
        public void Init()
        {
            _mgr = new InventoryManager();
            _itm = new InventoryItem
            {
                InventoryLabel = "Label1",
                CustomDetails = "My test PC",
                ItemType = InventoryType.PersonalComputer,
                ExperiationDate = DateTime.Now
            };
            _mgr.AddItem(_itm);
        }
        
        [TestMethod]
        public void AddItemTest()
        {
            _itm.InventoryLabel = "test";
            var ret = _mgr.AddItem(_itm);
            Assert.AreEqual(ret, true);
        }

        [TestMethod]
        public void AddNullItemTest()
        {
            
            var ret = _mgr.AddItem(null);
            Assert.AreEqual(ret, false);
        }

        [TestMethod]
        public void NotifyRemoveTest()
        {
            _mgr.Notify(NotificationType.ItemRemoved, "Test Message");
        }

        [TestMethod]
        public void NotifyExpirationTest()
        {
            _mgr.Notify(NotificationType.ItemExpired, "Test Message");
        }


        [TestMethod]
        public void GetItemsTest()
        {
            InventoryItem[] itms = _mgr.GetItems();
            Assert.AreEqual(1, itms.Length);
        }

        [TestMethod]
        public void GetItemTest()
        {
            var itmm = _mgr.GetItem(_itm.InventoryLabel);
            Assert.AreEqual(itmm.InventoryLabel, _itm.InventoryLabel);
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            var val = _mgr.RemoveItem(_itm.InventoryLabel);
            Assert.AreEqual(val, true);
        }

        [TestMethod]
        public void RemoveNullItemTest()
        {
            var val = _mgr.RemoveItem(null);
            Assert.AreEqual(val, false);
        }

        [TestMethod]
        public void NotifyExpiredItemsTest()
        {
            Task.Delay(1000);
            var itms = new List<InventoryItem>(_mgr.CheckItems());
            _mgr.NotifyExpiredItems();
            Assert.AreEqual(1, itms.Count);
        }

    }
}
