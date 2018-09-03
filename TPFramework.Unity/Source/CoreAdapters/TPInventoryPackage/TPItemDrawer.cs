/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Reflection;
using TPFramework.Core;
using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity
{
    [CustomPropertyDrawer(typeof(TPItem))]
    internal class TPItemDrawer : PropertyDrawer
    {
        private readonly BindingFlags privateInstance = BindingFlags.Instance | BindingFlags.NonPublic;
        private FieldInfo propertyField;
        private FieldInfo idField;
        private FieldInfo typeField;
        private FieldInfo nameField;
        private FieldInfo descriptionField;
        private FieldInfo worthField;
        private FieldInfo amountStackField;
        private FieldInfo maxStackField;
        private FieldInfo weightField;
        private FieldInfo modifiersField;
        private object target;

        private int ItemID { get { return (int)idField.GetValue(target); } set { idField.SetValue(target, value); } }
        private int ItemType { get { return (int)typeField.GetValue(target); } set { typeField.SetValue(target, value); } }
        private string ItemName { get { return (string)nameField.GetValue(target); } set { nameField.SetValue(target, value); } }
        private string ItemDescription { get { return (string)descriptionField.GetValue(target); } set { descriptionField.SetValue(target, value); } }
        private double ItemWorth { get { return (double)worthField.GetValue(target); } set { worthField.SetValue(target, value); } }
        private int ItemAmountStack { get { return (int)amountStackField.GetValue(target); } set { amountStackField.SetValue(target, value); } }
        private int ItemMaxStack { get { return (int)maxStackField.GetValue(target); } set { maxStackField.SetValue(target, value); } }
        private float ItemWeight { get { return (float)weightField.GetValue(target); } set { weightField.SetValue(target, value); } }

        private ITPModifier[] ItemModifiers {
            get { return (ITPModifier[])modifiersField.GetValue(target); }
            set { modifiersField.SetValue(target, value); }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (propertyField == null)
            {
                OnEnable(property);
            }
            position.height = EditorGUIUtility.singleLineHeight;

            ItemID = EditorGUI.IntField(position, "ID", ItemID);
            position.y += TPEditorGUI.fieldHeight;

            ItemType = EditorGUI.IntField(position, "Type", ItemType);
            position.y += TPEditorGUI.fieldHeight;

            ItemName = EditorGUI.TextField(position, "Name", ItemName);
            position.y += TPEditorGUI.fieldHeight;

            ItemDescription = EditorGUI.TextField(position, "Description", ItemDescription);
            position.y += TPEditorGUI.fieldHeight;

            ItemWorth = EditorGUI.DoubleField(position, "Worth", ItemWorth);
            position.y += TPEditorGUI.fieldHeight;

            ItemAmountStack = EditorGUI.IntField(position, "Amount Stack", ItemAmountStack);
            position.y += TPEditorGUI.fieldHeight;

            ItemMaxStack = EditorGUI.IntField(position, "Max Stack", ItemMaxStack);
            position.y += TPEditorGUI.fieldHeight;

            ItemWeight = EditorGUI.FloatField(position, "Weight", ItemWeight);
            position.y += TPEditorGUI.fieldHeight;

            //ItemModifiers = EditorGUI.IntField(position, "Modifiers", ItemModifiers);
            //position.y += EditorGUIUtility.singleLineHeight;
        }

        private void OnEnable(SerializedProperty property)
        {
            propertyField = property.serializedObject.targetObject.GetType().GetField(property.name);
            target = propertyField.GetValue(property.serializedObject.targetObject);
            idField = target.GetType().GetField("id", privateInstance);
            typeField = target.GetType().GetField("type", privateInstance);
            nameField = target.GetType().GetField("name", privateInstance);
            descriptionField = target.GetType().GetField("description", privateInstance);
            worthField = target.GetType().GetField("worth", privateInstance);
            amountStackField = target.GetType().GetField("amountStack", privateInstance);
            maxStackField = target.GetType().GetField("maxStack", privateInstance);
            weightField = target.GetType().GetField("weight", privateInstance);
            modifiersField = target.GetType().GetField("modifiers", privateInstance);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (property.CountInProperty() + 1);
        }
    }
}
