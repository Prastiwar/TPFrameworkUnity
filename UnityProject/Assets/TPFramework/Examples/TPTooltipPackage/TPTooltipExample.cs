using System.Collections;
using TP.Framework.Unity;
using UnityEngine;

public class TPTooltipExample : MonoBehaviour
{
    private bool isFading;
    private Coroutine fadeCor;
    [Required] public GameObject obj;
    [SerializeField] private float fadeSpeed;

    // Use this for initialization
    private void Start()
    {
        TPTooltipSystem.OnObserverEnter = FillTexts;
        TPTooltipSystem.OnObserverEnter += (tooltip) => ChangeState(tooltip, true);
        TPTooltipSystem.OnObserverExit = (tooltip) => ChangeState(tooltip, false);
    }

    private void FillTexts(TPTooltip tooltip)
    {
        DummyItem item = tooltip.GetComponent<DummyItem>();
        tooltip.TooltipLayout.GetText(0).SetText(item.Header);
        tooltip.TooltipLayout.GetText(1).SetText(item.Description);
        tooltip.TooltipLayout.GetButton(0).onClick.RemoveAllListeners();
        tooltip.TooltipLayout.GetButton(0).onClick.AddListener(() => Debug.Log("Click"));
    }

    private void ChangeState(TPTooltip tooltip, bool enter)
    {
        if (isFading)
        {
            StopCoroutine(fadeCor);
            tooltip.TooltipLayout.SetActive(!enter);
        }
        fadeCor = StartCoroutine(Fade(tooltip, enter));
    }

    private IEnumerator Fade(TPTooltip tooltip, bool active)
    {
        isFading = true;
        tooltip.TooltipLayout.SetAlpha(active ? 0 : 1);

        float alpha = tooltip.TooltipLayout.GetAlpha();
        while (active ? alpha < 1f : alpha > 0f)
        {
            yield return null;
            float speed = Time.deltaTime * fadeSpeed;
            alpha += active ? speed : -speed;
            tooltip.TooltipLayout.SetAlpha(alpha);
        }
        tooltip.TooltipLayout.SetActive(active);
        isFading = false;
    }
}
