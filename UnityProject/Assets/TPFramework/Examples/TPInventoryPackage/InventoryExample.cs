using System;
using TPFramework.Unity;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private TPInventory inventory;

    public void SpawnSlots()
    {
        TPSlotsSpawner spawner = GetComponent<TPSlotsSpawner>();
        if (spawner != null)
        {
            spawner.Clear();
            inventory.InitEquipSlots(spawner.SpawnEquipSlots());
            inventory.InitItemSlots(spawner.SpawnItemSlots());
        }
        else
        {
            throw new ArgumentNullException("There is no SlotsSpawner component attached to gameObject " + gameObject);
        }
    }

    private void Reset()
    {
        inventory = new TPInventory();
        SpawnSlots();
    }

    // Use this for initialization
    private void Start()
    {
        inventory.AddItem(inventory.GetItemHolder(0).Item);
        inventory.AddItem(inventory.GetItemHolder(1).Item);
    }
}
