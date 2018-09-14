/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Reflection;
using UnityEditor;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(InspectorValidateAttribute))]
    public class InspectorValidatePropertyDrawer : ValidateCallbackPropertyDrawer<InspectorValidateAttribute>
    {
        protected override void OnValidation(MethodInfo callback, object[] parameters, object fieldValue)
        {
            bool isValid = callback.Invoke(TargetObject, parameters).GetBool();
            if (!isValid)
            {
                ShowError(Attribute.Message ?? $"{Property.name} is not valid");
            }
        }

        protected override string GetCallbackRequirement()
        {
            return base.GetCallbackRequirement() + ", return bool";
        }
    }
}
