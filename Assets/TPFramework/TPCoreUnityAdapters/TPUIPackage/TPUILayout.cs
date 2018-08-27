using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using TPFramework.Core;

namespace TPFramework.Unity
{
    internal static class TPUI { } // marker to find this script

    /* ---------------------------------------------------------------------------- Layout Definition ---------------------------------------------------------------------------- */

    /// <summary>
    /// Layout Hierarchy:
    /// (TPLayout)
    ///     - (LayoutTransform)
    ///         - (Images parent)
    ///             - Image
    ///             - ...
    ///         - (Buttons parent)
    ///             - Button
    ///             - ...
    ///         - (Texts parent)
    ///             - Text
    ///             - ...
    /// </summary>
    [Serializable]
    public class TPUILayout : ITPUI
    {
        public GameObject LayoutPrefab;                         // Prefab to be isntantiated and assigned to TPLayout
        public GameObject TPLayout { get; set; }                // Instantiated prefab
        public Transform LayoutTransform { get; private set; }  // Child of TPLayout, have Image & Button & Text parents
        public CanvasGroup CanvasGroup { get; private set; }
        public bool IsInitialized { get { return TPLayout; } }

        protected Image[] Images { get; private set; }            // All Image components got from all childs of Image parent
        protected Button[] Buttons { get; private set; }          // All Button components got from all childs of Button parent
        protected TextMeshProUGUI[] Texts { get; private set; }   // All TextMeshProUGUI components got from all childs of Text parent

        /// <summary> Is called after Initialize </summary>
        protected virtual void OnInitialized() { }

        /// <summary> Returns if TPLayout is already spawned - if returns false, instantiate prefab on InitializeIfIsNot() </summary>
        protected virtual bool LayoutSpawn(Transform parent = null) { return IsInitialized; }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to TPLayout and get Images & Buttons & Texts </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public void Initialize()
        {
            Initialize(null);
        }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to TPLayout and get Images & Buttons & Texts </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public void Initialize(Transform parent)
        {
            if (IsInitialized)
                return;

            if (!LayoutSpawn(parent))
            {
                TPLayout = UnityEngine.Object.Instantiate(LayoutPrefab, parent);
            }
            InitializeLayout();
            OnInitialized();
        }

        /// <summary> If you set activation frequently, you'll want to avoid GC from eventsystem, use it instead of TPLayout.SetActive(..) </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public void SetActive(bool enable)
        {
            CanvasGroup.alpha = enable ? 1 : 0;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool IsActive()
        {
            return CanvasGroup.alpha == 1 && TPLayout.activeSelf;
        }

        /// <summary> Get Image & Buttons & Texts components from childs of parents </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        private void InitializeLayout()
        {
            LayoutTransform = TPLayout.transform.GetChild(0);
#if TPUISafeChecks
            SafeCheck(LayoutTransform);
#endif
            CanvasGroup = TPLayout.GetComponent<CanvasGroup>();
            Images = Initialize(LayoutTransform.GetChild(0), Images);
            Buttons = Initialize(LayoutTransform.GetChild(1), Buttons);
            Texts = Initialize(LayoutTransform.GetChild(2), Texts);
        }

#if TPUISafeChecks

        private void SafeCheck(Transform transform)
        {
            if (transform.childCount < 3)
                throw new Exception("Invalid TPUILayout! LayoutTransform needs to have Child 0: Parent of Images, Child 1: Parent of Buttons, Child 2: Parent of Texts");
            else if (transform.parent.GetComponent<CanvasGroup>() == null)
                throw new Exception("Invalid TPUILayout! TPLayout needs CanvasGroup component");
            else if (transform.GetChild(0).GetChilds().Any(x => x.GetComponent<Image>() == null))
                throw new Exception("Invalid TPUILayout! Child 0: Parent of Images must contain only Images as childs");
            else if (transform.GetChild(1).GetChilds().Any(x => x.GetComponent<Button>() == null))
                throw new Exception("Invalid TPUILayout! Child 1: Parent of Buttons must contain only Buttons as childs");
            else if (transform.GetChild(2).GetChilds().Any(x => x.GetComponent<TextMeshProUGUI>() == null))
                throw new Exception("Invalid TPUILayout! Child 2: Parent of Texts must contain only TextMeshProUGUIs as childs");
        }

#endif

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        private T[] Initialize<T>(Transform child, T[] array)
        {
            int length = child.childCount;
            array = new T[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = child.GetChild(i).GetComponent<T>();
            }
            return array;
        }
    }

    /* ---------------------------------------------------------------------------- Modal Window ---------------------------------------------------------------------------- */

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

        public void SetHeaderText(string text)
        {
            headerText.text = text;
        }

        public void SetDescriptionText(string text)
        {
            descriptionText.text = text;
        }

        public void SetAcceptText(string text)
        {
            acceptText.text = text;
        }

        public void SetCancelText(string text)
        {
            cancelText.text = text;
        }

        public void Show()
        {
            OnShow();
        }

        public void Hide()
        {
            OnHide();
        }

    }
}
