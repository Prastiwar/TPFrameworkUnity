using System.Collections.Generic;
using TPFramework.Unity;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private TPFramework.Unity.TPInventory inventory;

    private void Reset()
    {
        SlotsSpawner spawner = GetComponent<SlotsSpawner>();
        if (spawner != null)
        {
            List<TPEquipSlot> equipSlots = new List<TPEquipSlot>(4);
            List<TPItemSlot> itemSlots = new List<TPItemSlot>(8);

            spawner.Spawn(equipSlots, itemSlots);
            inventory.SetEquipSlots(equipSlots.ToArray());
            inventory.SetItemSlots(itemSlots.ToArray());
        }
    }

    // Use this for initialization
    private void Start()
    {

    }
}
