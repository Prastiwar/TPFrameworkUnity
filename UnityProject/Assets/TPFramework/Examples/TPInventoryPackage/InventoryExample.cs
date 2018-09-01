using System.Collections.Generic;
using TPFramework.Core;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private TPFramework.Unity.TPInventory inventory;

    private void Reset()
    {
        SlotsSpawner spawner = GetComponent<SlotsSpawner>();
        if (spawner != null)
        {
            List<ITPEquipSlot> equipSlots = new List<ITPEquipSlot>(4);
            List<ITPItemSlot> itemSlots = new List<ITPItemSlot>(8);

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
