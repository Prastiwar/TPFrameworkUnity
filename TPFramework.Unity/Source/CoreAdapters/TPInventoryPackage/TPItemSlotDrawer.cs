/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity
{
    [CustomPropertyDrawer(typeof(Core.TPItemSlot))]
    internal class TPItemSlotDrawer : PropertyDrawer
    {
        private readonly BindingFlags privateInstance = BindingFlags.Instance | BindingFlags.NonPublic;
        private FieldInfo propertyField;
        private FieldInfo typeField;
        private FieldInfo itemField;
        private object target;

        private int SlotType { get { return (int)typeField.GetValue(target); } set { typeField.SetValue(target, value); } }

        private TPItemHolder SlotItem {
            get {
                Core.TPItem item = (Core.TPItem)itemField.GetValue(target);
                return item == null ? null : TPEditorHelper.FindAssetByType<TPItemHolder>(x => x.Item.ID == item.ID);
            }
            set { itemField.SetValue(target, (Core.TPItem)value); }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (propertyField == null)
            {
                OnEnable(property);
            }
            position.height = EditorGUIUtility.singleLineHeight;
            SlotType = EditorGUI.IntField(position, "Type", SlotType);
            position.y += TPEditorGUI.fieldHeight;
            SlotItem = (TPItemHolder)EditorGUI.ObjectField(position, "Stored Item", SlotItem, typeof(TPItemHolder), false);
        }

        private void OnEnable(SerializedProperty property)
        {
            propertyField = property.serializedObject.targetObject.GetType().GetField(property.name);
            target = propertyField.GetValue(property.serializedObject.targetObject);
            typeField = target.GetType().GetField("type", privateInstance);
            itemField = target.GetType().GetField("storedItem", privateInstance);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (property.CountInProperty() + 1);
        }
    }
}
