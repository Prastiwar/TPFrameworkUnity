using System.Collections;
using TPFramework.Unity;
using UnityEngine;

public class AudioExample : MonoBehaviour
{
    [SerializeField] private TPAudioBundle audioBundle;
    [SerializeField] private int repeatCount = 5;

    private void Reset()
    {
        repeatCount = 5;
    }

    // Use this for initialization
    private void Start()
    {
        TPAudio.AddToPool("MyBundle", audioBundle);
        StartCoroutine(TPAudioPoolRepeatPlaying(repeatCount));
    }

    private IEnumerator TPAudioPoolRepeatPlaying(int repeat)
    {
        while (repeat >= 0)
        {
            TPAudio.Play("MyBundle", "door", () => {
                ExampleHelper.MessageWithLines("TPAudioPool Sound 'door' was played by MyBundle");
            });
            repeat--;
            yield return ExampleHelper.WaitSecond;
        }
    }
}
