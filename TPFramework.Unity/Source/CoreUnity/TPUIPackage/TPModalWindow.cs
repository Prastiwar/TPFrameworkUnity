/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPModalWindow : TPUILayout
    {
        private TextMeshProUGUI headerText;
        private TextMeshProUGUI descriptionText;
        private TextMeshProUGUI acceptText;
        private TextMeshProUGUI cancelText;
        private Button acceptButton;
        private Button cancelButton;

        public Action OnAccept = delegate { };
        public Action OnCancel = delegate { };
        public Action OnShow = delegate { };
        public Action OnHide = delegate { };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnInitialized()
        {
            headerText = Texts[0];
            descriptionText = Texts[1];
            acceptButton = Buttons[0];
            cancelButton = Buttons[1];
            acceptText = Buttons[0].GetComponentInChildren<TextMeshProUGUI>();
            cancelText = Buttons[1].GetComponentInChildren<TextMeshProUGUI>();

            OnAccept = Hide;
            OnCancel = Hide;
            OnShow = () => LayoutTransform.gameObject.SetActive(true);
            OnHide = () => LayoutTransform.gameObject.SetActive(false);
            acceptButton.onClick.AddListener(() => OnAccept());
            cancelButton.onClick.AddListener(() => OnCancel());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetHeaderText(string text)
        {
            headerText.text = text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDescriptionText(string text)
        {
            descriptionText.text = text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAcceptText(string text)
        {
            acceptText.text = text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCancelText(string text)
        {
            cancelText.text = text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Show()
        {
            OnShow();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Hide()
        {
            OnHide();
        }
    }
}
