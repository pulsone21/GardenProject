using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public enum ShopType { Hardwarestore, Foodmarket, Generalstore }
    public class ShopManager : InventoryManager
    {
        [SerializeField] private ShopType m_ShopType;

        private void Awake()
        {
            Inventory = new Inventory();
        }

    }
}
