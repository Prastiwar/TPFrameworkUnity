/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TP.Framework.Unity.UI
{
    /// <summary>
    /// Layout Hierarchy:
    /// (UIWindow)
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

        protected CanvasGroup CanvasGroup { get; private set; }             // CanvasGroup component attached to UIWindow
        protected Transform LayoutTransform { get; private set; }           // Child of UIWindow, have Image & Button & Text parents
        protected RectTransform LayoutRectTransform { get; private set; }   // Child of UIWindow, have Image & Button & Text parents
        protected RectTransform UIWindowRectTransform { get; private set; } // Child of UIWindow, have Image & Button & Text parents
        protected GameObject UIWindow { get; set; }                         // Instantiated prefab

        public GameObject UIWindowPrefab; // Prefab to be instantiated and assigned to UIWindow

        public bool IsInitialized { get { return UIWindow; } }

        /// <summary> Is called after Initialize </summary>
        protected virtual void OnInitialized() { }

        /// <summary> Returns if UIWindow is already spawned - if returns false, instantiate prefab on InitializeIfIsNot() </summary>
        protected virtual bool LayoutSpawn(Transform parent = null) { return IsInitialized; }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to UIWindow and get Images & Buttons & Texts </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize()
        {
            Initialize(null);
        }

        /// <summary> If IsInitialized is false - instantiate LayoutPrefab to UIWindow and get Images & Buttons & Texts </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(Transform parent)
        {
            if (IsInitialized)
                return;

            if (!LayoutSpawn(parent))
            {
                UIWindow = UnityEngine.Object.Instantiate(UIWindowPrefab, parent);
            }
            InitializeLayout();
            SetActive(false);
            OnInitialized();
        }

        /// <summary> If you set activation frequently, you'll want to avoid GC from eventsystem, use it instead of UIWindow.SetActive(..) </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetActive(bool enable)
        {
            CanvasGroup.alpha = enable ? 1 : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsActive()
        {
            return CanvasGroup.alpha >= 1 && UIWindow.activeSelf;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInteractable(bool interactable)
        {
            CanvasGroup.interactable = interactable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInteractable()
        {
            return CanvasGroup.interactable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBlocksRaycasts(bool blocksRaycast)
        {
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool BlocksRaycasts()
        {
            return CanvasGroup.blocksRaycasts;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAlpha(float alpha)
        {
            CanvasGroup.alpha = alpha;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetAlpha()
        {
            return CanvasGroup.alpha;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transform GetTransform()
        {
            return LayoutTransform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TextMeshProUGUI GetText(int index)
        {
            return Texts[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Image GetImage(int index)
        {
            return Images[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Button GetButton(int index)
        {
            return Buttons[index];
        }

        /// <summary> Get Image & Buttons & Texts components from childs of parents </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeLayout()
        {
            try
            {
                LayoutTransform = UIWindow.transform.GetChild(0);
            }
            catch (Exception)
            {
                Debug.LogError("You should have child transform on your prefab!");
            }
            SafeCheck(LayoutTransform);

            LayoutRectTransform = LayoutTransform.GetComponent<RectTransform>();
            UIWindowRectTransform = UIWindow.GetComponent<RectTransform>();
            CanvasGroup = UIWindow.GetComponent<CanvasGroup>();
            Images = Initialize(LayoutTransform.GetChild(0), Images);
            Buttons = Initialize(LayoutTransform.GetChild(1), Buttons);
            Texts = Initialize(LayoutTransform.GetChild(2), Texts);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SafeCheck(Transform transform)
        {
            if (transform.childCount < 3)
                throw new Exception("Invalid TPUILayout! LayoutTransform needs to have Child 0: Parent of Images, Child 1: Parent of Buttons, Child 2: Parent of Texts");
            else if (transform.parent.GetComponent<CanvasGroup>() == null)
                throw new Exception("Invalid TPUILayout! UIWindow needs CanvasGroup component");
            else if (transform.GetChild(0).AnyChildMatch(x => x.GetComponent<Image>() == null))
                throw new Exception("Invalid TPUILayout! Child 0: Parent of Images must contain only Images as childs");
            else if (transform.GetChild(1).AnyChildMatch(x => x.GetComponent<Button>() == null))
                throw new Exception("Invalid TPUILayout! Child 1: Parent of Buttons must contain only Buttons as childs");
            else if (transform.GetChild(2).AnyChildMatch(x => x.GetComponent<TextMeshProUGUI>() == null))
                throw new Exception("Invalid TPUILayout! Child 2: Parent of Texts must contain only TextMeshProUGUIs as childs");
        }

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
