using System;
using TP.Framework.Unity;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public void SpawnSlots()
    {
        SlotsSpawnBehaviour spawner = GetComponent<SlotsSpawnBehaviour>();
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
        inventory = new Inventory();
        SpawnSlots();
    }

    // Use this for initialization
    private void Start()
    {
        inventory.AddItem(inventory.GetItemHolder(0).Item);
        inventory.AddItem(inventory.GetItemHolder(1).Item);
    }
}
