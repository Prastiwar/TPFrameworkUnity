using TP.Framework;
using TP.Framework.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PersistenceExample : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private Button saveButton;

    [Persistant("SomeKey", "default")]
    [SerializeField] private string someString = "Text";

    [Persistant("SomeKey2", false)]
    [SerializeField] private bool someBool;

    [Persistant("SomeKey3", 10)]
    [SerializeField] private int someInt = 150;

    [Persistant("SomeKey4", 2.55f)]
    [SerializeField] private float someFloat = 10.25f;

    // Use this for initialization
    private void Start()
    {
        loadButton.onClick.AddListener(Load);
        saveButton.onClick.AddListener(Save);
    }

    private void Load()
    {
        TPPersistPrefs.Load(this);
        ExampleHelper.DrawLine();
        Debug.Log("Values Loaded");
        someString.ToLog("SomeString: ");
        someBool.ToLog("SomeBool: ");
        someInt.ToLog("SomeInt: ");
        someFloat.ToLog("SomeFloat: ");
        ExampleHelper.DrawLine();
    }

    private void Save()
    {
        TPPersistPrefs.Save(this);
        ExampleHelper.DrawLine();
        Debug.Log("Values Saved");
        someString.ToLog("SomeString: ");
        someBool.ToLog("SomeBool: ");
        someInt.ToLog("SomeInt: ");
        someFloat.ToLog("SomeFloat: ");
        ExampleHelper.DrawLine();
    }
}
