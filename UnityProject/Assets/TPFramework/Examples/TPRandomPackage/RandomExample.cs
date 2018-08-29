using System.Collections;
using TPFramework.Core;
using UnityEngine;

public class RandomExample : MonoBehaviour
{
    private ProbabilityElementInt<GameObject>[] probabilityElements;

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private int repeatCount = 5;

    // Use this for initialization
    private void Start()
    {
        int elLength = gameObjects.Length;
        probabilityElements = new ProbabilityElementInt<GameObject>[elLength];
        int[] randomProbabilities = TPRandom.RandomProbabilities(elLength);

        ExampleHelper.DrawLine();
        for (int i = 0; i < elLength; i++)
        {
            probabilityElements[i] = new ProbabilityElementInt<GameObject>(gameObjects[i], randomProbabilities[i]);
            Debug.Log("Random probability: " + probabilityElements[i].Probability);
        }
        ExampleHelper.DrawLine();

        StartCoroutine(TPRandomToggleObject(repeatCount));
    }

    private IEnumerator TPRandomToggleObject(int repeat)
    {
        while (repeat >= 0)
        {
            GameObject selectedObject = TPRandom.PickWithProbability(probabilityElements);
            selectedObject.SetActive(!selectedObject.activeSelf);
            repeat--;
            yield return ExampleHelper.WaitSecond;
        }
    }
}
