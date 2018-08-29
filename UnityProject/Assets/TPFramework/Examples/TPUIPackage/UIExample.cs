using TPFramework.Unity;
using UnityEngine;
using UnityEngine.UI;

public class UIExample : MonoBehaviour
{
    private bool windowEnabled;

    [SerializeField] private Button toggleWindowBtn;
    [SerializeField] private TPModalWindow modalWindow;

    // Use this for initialization
    private void Start()
    {
        modalWindow.Initialize();
        windowEnabled = false;
        modalWindow.OnShow = () => CustomModalWindowPop();
        modalWindow.OnHide = () => CustomModalWindowPop();

        toggleWindowBtn.onClick.AddListener(() => {
            windowEnabled = !windowEnabled;
            if (windowEnabled)
                modalWindow.Show();
            else
                modalWindow.Hide();
        });
    }

    private void CustomModalWindowPop(/*float evaluatedTime, Transform window*/)
    {
        //window.localScale = window.localScale.Equal(TPAnim.ReflectNormalizedCurveTime(evaluatedTime));
    }
}
