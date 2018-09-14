/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Reflection;
using UnityEditor;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(OnValueChangedAttribute))]
    public class OnValueChangedPropertyDrawer : ValidateCallbackPropertyDrawer<OnValueChangedAttribute>
    {
        private object cachedValue;

        protected override void OnValidation(MethodInfo callback, object[] parameters, object fieldValue)
        {
            if (!CheckEqual(cachedValue, fieldValue))
            {
                cachedValue = fieldValue;
                callback.Invoke(TargetObject, parameters);
            }
        }

        private bool CheckEqual(object cachedValue, object fieldValue)
        {
            if (cachedValue != null)
            {
                return cachedValue.Equals(fieldValue);
            }
            else if (fieldValue != null)
            {
                return fieldValue.Equals(cachedValue);
            }
            return true;
        }

        protected override string GetCallbackRequirement()
        {
            return base.GetCallbackRequirement() + ", return void";
        }
    }
}
