using System.Collections.Generic;
using TPFramework.Unity;
using UnityEngine;

public class SlotsSpawner : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private int equipSlotsCount;
    [SerializeField] private int itemSlotsCount;
    [SerializeField] private GameObject equipSlotPrefab;
    [SerializeField] private GameObject itemSlotPrefab;

    public void Spawn(List<TPEquipSlot> equipSlots, List<TPItemSlot> itemSlots)
    {
        if (inventoryPanel == null || equipSlotPrefab == null || itemSlotPrefab == null)
            return;

        Clear();
        equipSlots.Clear();
        itemSlots.Clear();

        for (int i = 0; i < equipSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(equipSlotPrefab, inventoryPanel);
            TPEquipSlot slotComponent = slotObject.GetComponent<TPEquipSlot>();
            equipSlots.Add(slotComponent);
        }

        for (int i = 0; i < itemSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, inventoryPanel);
            TPItemSlot slotComponent = slotObject.GetComponent<TPItemSlot>();
            itemSlots.Add(slotComponent);
        }
    }

    public void Clear()
    {
        int length = inventoryPanel.childCount;
        for (int i = 0; i < length; i++)
        {
            DestroyImmediate(inventoryPanel.GetChild(0).gameObject);
        }
    }
}
