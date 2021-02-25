/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : ValidatePropertyDrawer<RequiredAttribute>
    {
        protected override void Validate()
        {
            if (Property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (Property.objectReferenceValue == null)
                {
                    ShowError(Attribute.Message ?? Property.name + " is required");
                }
            }
            else
            {
                ShowWarning(Attribute.GetType().Name + " works only on reference types");
            }
        }
    }
}
