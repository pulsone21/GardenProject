using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace InventorySystem
{
    public abstract class InventoryManager : MonoBehaviour
    {
        public Inventory Inventory { get; protected set; }
        [SerializeField] protected int m_money;
        [SerializeField] protected int m_InventoryPlaces;
        public Action OnMoneyChange;

        [SerializeField] protected GameObject m_itemCard;

        public void RegisterOnMoneyChange(Action action) => OnMoneyChange += action;
        public void UnregisterOnMoneyChange(Action action) => OnMoneyChange -= action;

        public virtual int GetItemAmount(IInventoryObject inventoryObject) => Inventory.GetIventoryObjectAmount(inventoryObject);

    }
}