using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InventorySystem
{
    [SerializeField]
    public class Inventory
    {

        private List<InventoryPlace> m_InventorySlots;

        private Action m_OnIssue;
        private Action m_OnReceive;

        public Inventory(List<InventoryPlace> items)
        {
            m_InventorySlots = new List<InventoryPlace>();
            //? this resolves possible duplicates from the incoming list
            foreach (InventoryPlace IP in items)
            {
                InventoryPlace ipInList = m_InventorySlots.Find(x => x.Name == IP.Name);
                if (ipInList != null)
                {
                    ipInList.Add(IP.CurrentAmount);
                }
                else
                {
                    m_InventorySlots.Add(IP);
                }
            }
        }

        public Inventory()
        {
            m_InventorySlots = new List<InventoryPlace>();
        }

        public void RegisterForOnIssue(Action action) => m_OnIssue += action;
        public void RegisterForOnReceive(Action action) => m_OnReceive += action;
        public void UnregisterForOnIssue(Action action) => m_OnIssue -= action;
        public void UnregisterForOnReceive(Action action) => m_OnReceive -= action;

        public bool IsObjectAvailable(IInventoryObject _InventoryObject) => GetIventoryObjectAmount(_InventoryObject) > 0;

        public InventoryPlace[] GetAllObjects() => m_InventorySlots.ToArray();

        public int GetIventoryObjectAmount(IInventoryObject _InventoryObject)
        {
            foreach (InventoryPlace InventoryPlace in m_InventorySlots)
            {
                if (InventoryPlace.Object.Name == _InventoryObject.Name)
                {
                    return InventoryPlace.CurrentAmount;
                }
            }
            Debug.Log($"Inventory does not contain {_InventoryObject}");
            return 0;
        }

        public bool IssueInventoryObject(IInventoryObject _InventoryObject, int amount)
        {
            foreach (InventoryPlace IP in m_InventorySlots)
            {
                if (IP.Object.Name == _InventoryObject.Name)
                {
                    bool state = IP.Remove(amount);
                    if (state)
                    {
                        m_OnIssue?.Invoke();
                        return true;
                    }
                }
            }
#if !UNITY_INCLUDE_TESTS
            // ProdCode specific code
            UISystem.TooltipManager.Instance.Initialize($"Cannot store {_InventoryObject}", 1.5f);
#endif
            return false;
        }

        public bool ReceiveInventoryObject(IInventoryObject _InventoryObject, int _amount)
        {
            foreach (InventoryPlace IP in m_InventorySlots)
            {
                if (IP.Object.Name == _InventoryObject.Name)
                {
                    IP.Add(_amount);
                    m_OnReceive?.Invoke();
                    return true;
                }
            }
            m_InventorySlots.Add(new InventoryPlace(_InventoryObject, _amount));
            m_OnReceive?.Invoke();
            return true;
        }


    }
}
