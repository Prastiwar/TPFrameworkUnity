using TPFramework.Unity;
using UnityEngine;

public class AttributeExample : MonoBehaviour
{
    [SerializeField] private TPAttribute health;
    [SerializeField] private TPModifier healthIncreaser;

    // Use this for initialization
    private void Start()
    {
        health.Recalculate(); // we need to call this since changes from editor doesnt call it
        object goldenArmor = new object(); // some Item
        healthIncreaser.Source = goldenArmor;
        ExampleHelper.DrawLine();
        Debug.Log("TPAttribute Health Base value: " + health.BaseValue);
        Debug.Log("TPAttribute Health Value before armor equip: " + health.Value);
        health.Modifiers.Add(healthIncreaser);
        Debug.Log("TPAttribute Health Value after armor equip: " + health.Value);
        ExampleHelper.DrawLine();
    }
}
