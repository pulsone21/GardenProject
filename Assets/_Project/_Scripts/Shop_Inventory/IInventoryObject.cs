
using UnityEngine;

namespace InventorySystem
{
    public interface IInventoryObject
    {
        Sprite UiVisual { get; }
        int Cost { get; }
        string Name { get; }
        void PickItem(Inventory inventory);
    }
}