using System.Collections.Generic;
using TPFramework.Unity;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private TPInventory inventory;

    private void Reset()
    {
        SlotsSpawner spawner = GetComponent<SlotsSpawner>();
        if (spawner != null)
        {
            List<TPEquipSlotHolder> equipSlotsList = new List<TPEquipSlotHolder>(4);
            List<TPItemSlotHolder> itemSlotsList = new List<TPItemSlotHolder>(8);

            spawner.Spawn(equipSlotsList, itemSlotsList);
            inventory = new TPInventory();
            inventory.InitEquipSlots(equipSlotsList.ToArray());
            inventory.InitItemSlots(itemSlotsList.ToArray());
        }
    }

    // Use this for initialization
    private void Start()
    {
        inventory.AddItem(inventory.ItemDatabase.GetItemHolder(0).Item);
    }
}
