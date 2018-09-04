using System.Collections.Generic;
using TPFramework.Unity;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private TPInventory inventory;

    private void Reset()
    {
        inventory = new TPInventory();
        SpawnSlots();
    }

    public void SpawnSlots()
    {
        SlotsSpawner spawner = GetComponent<SlotsSpawner>();
        if (spawner != null)
        {
            List<TPEquipSlotHolder> equipSlotsList = new List<TPEquipSlotHolder>(4);
            List<TPItemSlotHolder> itemSlotsList = new List<TPItemSlotHolder>(8);

            spawner.Spawn(equipSlotsList, itemSlotsList);
            inventory.InitEquipSlots(equipSlotsList.ToArray());
            inventory.InitItemSlots(itemSlotsList.ToArray());
        }
        else
        {
            throw new System.ArgumentNullException("There is no SlotsSpawner component attached to gameObject " + gameObject);
        }
    }

    public void InitializeDatabase(TPItemHolder[] itemHolders)
    {
        inventory.ItemDatabase.InitDatabase(itemHolders);
    }

    // Use this for initialization
    private void Start()
    {
        inventory.AddItem(inventory.ItemDatabase.GetItemHolder(0).Item);
    }
}
