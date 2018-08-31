/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
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
        protected Image[] Images { get; private set; }           // All Image components got from all childs of Image parent
        protected Button[] Buttons { get; private set; }         // All Button components got from all childs of Button parent
        protected TextMeshProUGUI[] Texts { get; private set; }  // All TextMeshProUGUI components got from all childs of Text parent

        public GameObject LayoutPrefab;                         // Prefab to be instantiated and assigned to TPLayout

        public GameObject TPLayout { get; set; }                // Instantiated prefab
        public Transform LayoutTransform { get; private set; }  // Child of TPLayout, have Image & Button & Text parents
        public CanvasGroup CanvasGroup { get; private set; }    // CanvasGroup component attached to TPLayout
        public bool IsInitialized { get { return TPLayout; } }

        /// <summary> Is called after Initialize </summary>
        protected virtual void OnInitialized() { }

        /// <summary> Returns if TPLayout is already spawned - if returns false, instantiate prefab on InitializeIfIsNot() </summary>
        protected virtual bool LayoutSpawn(Transform parent = null) { return IsInitialized; }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to TPLayout and get Images & Buttons & Texts </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize()
        {
            Initialize(null);
        }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to TPLayout and get Images & Buttons & Texts </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(Transform parent)
        {
            if (IsInitialized)
                return;

            if (!LayoutSpawn(parent))
            {
                TPLayout = UnityEngine.Object.Instantiate(LayoutPrefab, parent);
            }
            InitializeLayout();
            SetActive(false);
            OnInitialized();            
        }

        /// <summary> If you set activation frequently, you'll want to avoid GC from eventsystem, use it instead of TPLayout.SetActive(..) </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetActive(bool enable)
        {
            CanvasGroup.alpha = enable ? 1 : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsActive()
        {
            return CanvasGroup.alpha >= 1 && TPLayout.activeSelf;
        }

        /// <summary> Get Image & Buttons & Texts components from childs of parents </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeLayout()
        {
#if TPUISafeChecks
            try
            {
                LayoutTransform = TPLayout.transform.GetChild(0);
            }
            catch (Exception)
            {
                Debug.LogError("You should have child transform on your prefab!");
            }
            SafeCheck(LayoutTransform);
#else
            LayoutTransform = TPLayout.transform.GetChild(0);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
}
