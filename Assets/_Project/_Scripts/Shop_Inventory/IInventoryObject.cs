
using UnityEngine;

namespace InventorySystem
{
    public interface IInventoryObject
    {
        Sprite UiVisual { get; }
        int BaseCost { get; }
        string Name { get; }

        void PickItem(Inventory inventory);
    }
}