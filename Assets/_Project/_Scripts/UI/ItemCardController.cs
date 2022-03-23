using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace InventorySystem
{
    public class ItemCardController : MonoBehaviour
    {
        public enum Context { Sell, Buy, Pick }
        [SerializeField] private Image m_ItemImage;
        [SerializeField] private TextMeshProUGUI m_ItemName;
        [SerializeField] private TextMeshProUGUI m_ItemCosts;
        [SerializeField] private TextMeshProUGUI m_ItemAmount;
        [SerializeField] private GameObject m_SellBtn;
        [SerializeField] private GameObject m_BuyBtn;

        private IInventoryObject m_InventoryObject;
        private InventoryManager m_MyManager;

        public void InitializeItemCard(IInventoryObject InventoryObject, InventoryManager MyManager, Context context)
        {
            m_MyManager = MyManager;
            m_InventoryObject = InventoryObject;
            m_ItemImage.sprite = InventoryObject.UiVisual;
            RefreshCost();
            RefreshAmount();
            m_ItemName.text = InventoryObject.Name;
            EvalButtons(context);
            MyManager.Inventory.RegisterForOnIssue(RefreshAmount);
            MyManager.Inventory.RegisterForOnReceive(RefreshAmount);
        }

        private void EvalButtons(Context context)
        {
            switch (context)
            {
                case Context.Sell:
                    m_SellBtn.SetActive(true);
                    m_BuyBtn.SetActive(false);
                    break;
                case Context.Buy:
                    m_BuyBtn.SetActive(true);
                    m_SellBtn.SetActive(false);
                    break;
                case Context.Pick:
                    //? do nothing, buttons are disabled on start and no buttons are needed.
                    break;
                default: throw new NotImplementedException();
            }
        }

        public void RefreshInfos()
        {
            RefreshCost();
            RefreshAmount();
        }

        private void RefreshCost() => m_ItemCosts.text = CostString(m_InventoryObject.Cost); //TODO Implement some kind of economics
        private void RefreshAmount() => m_ItemAmount.text = AmountString(m_MyManager.GetItemAmount(m_InventoryObject));
        public void PickItem() => m_InventoryObject.PickItem(m_MyManager.Inventory);


        public void Buy()
        {
            //TODO Implement buying structure
            //we need to know from where to where
            Debug.Log("buying!");
        }

        public void Sell()
        {
            //TODO Implement selling structure
            //we need to know from where to where
            Debug.Log("Selling!");
        }

        private void OnDestroy()
        {
            m_MyManager.Inventory.UnregisterForOnIssue(RefreshInfos);
            m_MyManager.Inventory.UnregisterForOnReceive(RefreshInfos);
        }
        private string CostString(int costs) => $"Cost\n{costs.ToString()}";
        private string AmountString(int amount) => $"Amount\n{amount.ToString()}";
    }
}
