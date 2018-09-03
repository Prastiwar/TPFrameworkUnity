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

    public void Spawn(List<TPEquipSlotHolder> equipSlots, List<TPItemSlotHolder> itemSlots)
    {
        if (inventoryPanel == null || equipSlotPrefab == null || itemSlotPrefab == null)
            return;

        Clear();
        equipSlots.Clear();
        itemSlots.Clear();

        for (int i = 0; i < equipSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(equipSlotPrefab, inventoryPanel);
            TPEquipSlotHolder slotComponent = slotObject.GetComponent<TPEquipSlotHolder>();
            equipSlots.Add(slotComponent);
        }

        for (int i = 0; i < itemSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, inventoryPanel);
            TPItemSlotHolder slotComponent = slotObject.GetComponent<TPItemSlotHolder>();
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
