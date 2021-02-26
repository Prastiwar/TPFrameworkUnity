using System.Collections;
using TP.Framework.Unity;
using UnityEngine;
using UnityEngine.UI;

public class AudioExample : MonoBehaviour
{
    [SerializeField] private Button refreshButton;
    [SerializeField] private AudioBundle audioBundle;
    [SerializeField] private int repeatCount = 5;

    private void Reset()
    {
        repeatCount = 5;
    }

    private void Awake()
    {
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(Refresh);
        }
    }

    // Use this for initialization
    private void Start()
    {
        AudioSystem.AddToPool("MyBundle", audioBundle);
        StartCoroutine(TPAudioPoolRepeatPlaying(repeatCount));
    }

    private void Refresh()
    {
        StartCoroutine(TPAudioPoolRepeatPlaying(repeatCount));
    }

    private IEnumerator TPAudioPoolRepeatPlaying(int repeat)
    {
        while (repeat >= 0)
        {
            AudioSystem.Play("MyBundle", "Door", () => {
                ExampleHelper.MessageWithLines("TPAudioPool Sound 'Door' was played by MyBundle");
            });
            repeat--;
            yield return ExampleHelper.WaitSecond;
        }
    }
}
