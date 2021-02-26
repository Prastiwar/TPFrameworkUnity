/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Reflection;

namespace TP.Framework.Unity.Editor
{
    public class ValidateCallbackPropertyDrawer<TAttribute> : ValidatePropertyDrawer<TAttribute>
        where TAttribute : CallbackAttribute
    {
        protected MethodInfo Callback { get; private set; }
        protected object[] CallbackParameters { get; private set; }
        protected bool IsCallbackValid { get; private set; }

        protected override void OnEnabled()
        {
            Initialize();
        }

        protected override void Validate()
        {
            BaseValidate();
        }

        private void BaseValidate()
        {
            if (IsCallbackValid)
            {
                object[] parameters = null;
                object fieldValue = fieldInfo.GetValue(TargetObject);
                if (CallbackParameters != null)
                {
                    parameters = CallbackParameters;
                    CallbackParameters[0] = fieldValue;
                }
                OnValidation(Callback, parameters, fieldValue);
            }
            else
            {
                ShowWarning($"Callback {Attribute.CallbackName} is not valid. {GetCallbackRequirement()}");
            }
        }

        protected void Initialize()
        {
            Type classType = TargetObject.GetType();
            Type parameterType = fieldInfo.FieldType;
            int parametersLength = 0;
            try
            {
                Callback = classType.GetMethod(Attribute.CallbackName, findValueFlags);
            }
            catch (Exception) { }
            if (Callback != null)
            {
                ParameterInfo[] parameters = Callback.GetParameters();
                parametersLength = parameters.Length;
                if (parametersLength > 0)
                {
                    CallbackParameters = new object[1];
                    parameterType = parameters[0].ParameterType;
                }
            }
            IsCallbackValid = Callback != null && parametersLength <= 1 && parameterType == fieldInfo.FieldType;
        }

        protected virtual string GetCallbackRequirement()
        {
            return "Supported: No overload, 0 or 1 parameter matching field";
        }

        protected virtual void OnValidation(MethodInfo callback, object[] parameters, object fieldValue) { }
    }
}
