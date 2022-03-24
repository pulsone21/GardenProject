using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventoryPlace
    {
        public readonly IInventoryObject Object;
        public int CurrentAmount { get; protected set; }

        public InventoryPlace(IInventoryObject @object, int initalAmount)
        {
            Object = @object;
            CurrentAmount = initalAmount;
        }

        public string Name => Object.Name;

        public void Add(int _amount) => CurrentAmount += _amount;

        public bool Remove(int _amount)
        {
            if (CurrentAmount - _amount < 0)
            {
                return false;
            };
            CurrentAmount -= _amount;
            return true;
        }

    }
}
