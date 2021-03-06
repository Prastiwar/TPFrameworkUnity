﻿using TP.Framework.Unity;
using UnityEngine;
using UnityEngine.UI;

public class AttributeExample : MonoBehaviour
{
    [SerializeField] private Button refreshButton;
    [SerializeField] private ModifiableAttribute health;
    [SerializeField] private AttributeModifier healthIncreaser;

    private void Awake()
    {
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(Refresh);
        }
    }

    // Use this for initialization
    private void Start()
    {
        object goldenArmor = new object(); // some Item
        healthIncreaser.Source = goldenArmor;
        ExampleHelper.DrawLine();
        Debug.Log("TPAttribute Health Base value: " + health.BaseValue);
        Debug.Log("TPAttribute Health Value before armor equip(healthIncreaser): " + health.Value);
        health.Modifiers.Add(healthIncreaser);
        Debug.Log("TPAttribute Health Value after armor equip(healthIncreaser): " + health.Value);
        ExampleHelper.DrawLine();
    }

    private void Refresh()
    {
        ExampleHelper.MessageWithLines("TPAttribute Health Value: " + health.Value);
    }
}
