/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TPFramework.Unity;
using UnityEngine;

public class TPSlotsSpawner : MonoBehaviour
{
    [SerializeField] private int equipSlotsCount;
    [SerializeField] private int itemSlotsCount;
    [SerializeField] private GameObject equipSlotPrefab;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform equipSlotsPanel;
    [SerializeField] private Transform itemslotsPanel;

    /// <summary> Spawns inventory slots layout and add slots to list </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TPEquipSlotHolder[] SpawnEquipSlots()
    {
        return Spawn<TPEquipSlotHolder>(equipSlotsCount, equipSlotPrefab, equipSlotsPanel);
    }

    /// <summary> Spawns inventory slots layout and add slots to list </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TPItemSlotHolder[] SpawnItemSlots()
    {
        return Spawn<TPItemSlotHolder>(itemSlotsCount, itemSlotPrefab, itemslotsPanel);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearItemSlots()
    {
        itemslotsPanel.DestroyChildren();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearEquipSlots()
    {
        equipSlotsPanel.DestroyChildren();
    }

    /// <summary> Destroys all children in equipSlotsPanel AND itemslotsPanel </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        equipSlotsPanel.DestroyChildren();
        itemslotsPanel.DestroyChildren();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T[] Spawn<T>(int length, GameObject prefab, Transform panel)
    {
        if (panel == null || prefab == null)
        {
            return null;
        }
        List<T> slots = new List<T>(length);
        Instantiate(length, prefab, panel, slots);
        return slots.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Instantiate<T>(int length, GameObject prefab, Transform parent, List<T> slotsList)
    {
        for (int i = 0; i < length; i++)
        {
            GameObject slotObject = Instantiate(prefab, parent);
            T slotComponent = slotObject.GetComponent<T>();
            slotsList.Add(slotComponent);
        }
    }

    private void OnValidate()
    {
        equipSlotPrefab = equipSlotPrefab != null ? CheckNull<TPEquipSlotHolder>(equipSlotPrefab) : equipSlotPrefab;
        itemSlotPrefab = itemSlotPrefab != null ? CheckNull<TPItemSlotHolder>(itemSlotPrefab) : itemSlotPrefab;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GameObject CheckNull<T>(GameObject prefab)
    {
        T component = prefab.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"{prefab.name} Prefab should have {typeof(T)} component");
            prefab = null;
        }
        return prefab;
    }
}
