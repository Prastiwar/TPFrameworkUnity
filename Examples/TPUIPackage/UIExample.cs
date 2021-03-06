﻿using TP.Framework.Unity;
using TP.Framework.Unity.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIExample : MonoBehaviour
{
    [SerializeField] private Button toggleWindowBtn;
    [SerializeField] private ModalWindow modalWindow;

    // Use this for initialization
    private void Start()
    {
        modalWindow.Initialize();
        modalWindow.OnShow += CustomModalWindowPop;
        modalWindow.OnHide = CustomModalWindowPop;

        toggleWindowBtn.onClick.AddListener(() => {
            if (!modalWindow.IsActive())
                modalWindow.Show();
            else
                modalWindow.Hide();
        });
    }

    private void CustomModalWindowPop(float evaluatedTime, Transform window)
    {
        window.localScale = window.localScale.Set(evaluatedTime);
    }
}
