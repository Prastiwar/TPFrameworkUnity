using System.Collections.Generic;
using TPFramework.Core;
using TPFramework.Unity;
using UnityEngine;

public class SlotsSpawner : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private int equipSlotsCount;
    [SerializeField] private int itemSlotsCount;
    [SerializeField] private GameObject equipSlotPrefab;
    [SerializeField] private GameObject itemSlotPrefab;

    public void Spawn(List<ITPEquipSlot> equipSlots, List<ITPItemSlot> itemSlots)
    {
        if (inventoryPanel == null || equipSlotPrefab == null || itemSlotPrefab == null)
            return;

        Clear();
        equipSlots.Clear();
        itemSlots.Clear();

        for (int i = 0; i < equipSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(equipSlotPrefab, inventoryPanel);
            TPFramework.Unity.TPItemEquipSlot slotComponent = slotObject.GetComponent<TPFramework.Unity.TPItemEquipSlot>();
            TPFramework.Core.TPItemEquipSlot slot = slotComponent;
            equipSlots.Add(slot);
        }

        for (int i = 0; i < itemSlotsCount; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, inventoryPanel);
            TPFramework.Unity.TPItemSlot slotComponent = slotObject.GetComponent<TPFramework.Unity.TPItemSlot>();
            TPFramework.Core.TPItemSlot slot = slotComponent;
            itemSlots.Add(slot);
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
